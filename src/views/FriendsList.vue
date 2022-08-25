<!--2053382 范珑骁-->
<template>
  <div class="Friends">
    <el-card>
      <div class="top-text">好友列表</div>
      <!--搜索与添加区域-->
      <el-row :gutter="20">
        <el-col :span="4">
          <el-button type="primary" @click="searchById" class="add" icon="el-icon-circle-plus-outline" plain
          >添加好友</el-button
          >
        </el-col>
      </el-row>
      <el-table
          :data="tableData"
          border
          style="width: 90%"
          :header-cell-style="{
          'text-align': 'center',
          background: '#F5F5F5'
        }"
          :cell-style="{
          'text-align': 'center'
        }"
          max-height="330"
          empty-text="您还没有好友，快去发现同好吧~~~">
        <el-table-column
            fixed
            prop="id"
            label="用户id"
            width="120">
        </el-table-column>
        <el-table-column
            prop="profile_photo"
            label="头像"
            width="120">
          <template slot-scope="scope">
            <img :src="scope.row.profile_photo" width="60" height="60" @error="defImg" alt="头像"/>
          </template>
        </el-table-column>
        <el-table-column
            prop="name"
            label="昵称"
            width="110">
        </el-table-column>
        <el-table-column
            prop="status"
            label="在线状态"
            width="100">
          <template slot-scope="scope">
            <el-switch
                v-model="scope.row.status"
                disabled
            ></el-switch>
          </template>
        </el-table-column>
        <el-table-column
            fixed="right"
            label="操作"
            width="173">
          <!-- 查看按钮 -->
          <template slot-scope="scope">
            <el-button
                type="primary"
                icon="el-icon-tickets"
                size="mini"
                @click="getUsersList(scope.row.id)"
            ></el-button>
            <!-- 聊天按钮 -->
            <el-button
                type="success"
                icon="el-icon-chat-dot-round"
                size="mini"
                @click="openChat(scope.row.id,scope.row.name,scope.row.profile_photo)"
            ></el-button>
            <!-- 删除按钮 -->
            <el-button
                type="danger"
                icon="el-icon-delete"
                size="mini"
                @click="removeUserById(scope.row.id)"
            ></el-button>
          </template>
        </el-table-column>
      </el-table>
      <InfoDialog :form="form" :DialogVisible="DialogVisible_1" @callBack="callBack_1"></InfoDialog>
      <MessageCenter :chat="chat" :listData="listData" :DialogVisible="DialogVisible_2" @callBack="callBack_2"></MessageCenter>
    </el-card>
  </div>
</template>

<script>
import SearchUser from './SearchUser';
import InfoDialog from './InfoDialog';
import MessageCenter from "@/views/MessageCenter";
import { showLoading, hideLoading } from '@/components/LoadingSet/loading.js';
export default {
  name: "FriendsList",
  data(){
    return{
      DialogVisible_1: false,
      DialogVisible_2: false,
      defImg: require("../assets/avatar.jpg"),
      queryInfo:{
        id: ''
      },
      iframeData:{
        id: this.$route.params.id
      },
      listData:[],
      form:{
        profile_photo:'',
        id:'',
        name:'',
        email:'',
        birthday:'',
        game_num: 0,
        intro:''
      },
      chat:{
        id_A:this.$route.params.id,
        id_B:'',
        name_A: this.$route.params.name,
        name_B: '',
        avatar_A:this.$route.params.avatar,
        avatar_B:require('../assets/avatar_2.jpg')
      },
      tableData:[{
        id: '0000000002',
        name: 'Laura',
        status: false,
        profile_photo: require('../assets/avatar_1.jpg')
      }, {
        id: '0000000003',
        name: 'Sakuzyo',
        status: true,
        profile_photo: require('../assets/avatar_2.jpg')
      }, {
        id: '0000000004',
        name: 'Jiaran',
        status: true,
        profile_photo: require('../assets/avatar_3.jpg')
      }]
    };
  },
  components:{
    MessageCenter,
    SearchUser,
    InfoDialog
  },
  methods:{
    callBack_1(flag){
      this.DialogVisible_1 = flag
    },
    callBack_2(flag){
      this.DialogVisible_2 = flag
    },
    getUsersList(uid) {
      const self = this;
      showLoading()
      self.DialogVisible_1 = true;
      self.$axios({
        method: 'post',
        url: 'api/user/getUserInfo',
        data: {
          id: uid
        }
      })
          .then(res => {
            hideLoading()
            switch (res.data.result) {
              case 1:
                console.log("查找成功！");
                self.form.profile_photo = require('../../../ExGame-Asset/User/' + uid +'/ProfilePhoto.jpg');
                self.form.id = uid;
                self.form.name = res.data.name;
                self.form.email = res.data.email;
                self.form.birthday = res.data.birthday;
                self.form.game_num = res.data.game_num;
                self.form.intro = res.data.intro;
                break;
              case 0:
                console.log("查找失败！");
                break;
              case -1:
                console.log("获取数据出现问题！");
                break;
              case -2:
                alert("数据库连接失败！");
                break
            }
          })
          .catch(err => {
            console.log(err);
          })
    },
    searchById() {
      this.$layer.iframe({
        type: 2,
        title: "搜索用户",
        area: ['670px', '670px'],
        shade: true,
        offset: 'auto',
        content: {
          content: SearchUser,//传递的编辑组件主线
          parent: this,
          data: {
            iframeData:this.iframeData
          }
        },
        cancel:()=>{//关闭事件
          this.getFriendsList(0)
        }
      })
    },
    // 根据id删除对应的用户信息
    async removeUserById(uid) {
      // 弹框提示用户是否删除
      const confirmResult = await this.$confirm(
          '此操作将永久删除该用户, 是否继续?',
          '提示',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }
      ).catch(err => err)
      // 如果用户确认删除，则返回字符串 confirm
      // 如果用户取消删除，则返回字符串 cancel
      if (confirmResult !== 'confirm') {
        return this.$message.info('已取消删除')
      }
      // 发送删除请求
      showLoading()
      this.$axios({
        method: 'post',
        url: 'api/user/removeFriends',
        data: {
          id_A: this.iframeData.id,
          id_B: uid
        }
      })
          .then(res => {
            hideLoading()
            switch (res.data.result) {
              case 1:
                console.log("好友删除成功！");
                this.$layer.msg("好友删除成功！");
                this.getFriendsList(0)
                break;
              case 0:
                console.log("好友删除失败！");
                this.$layer.msg("好友删除失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break
            }
          })
          .catch(err => {
            console.log(err);
          })
    },
    // 根据id获取对应的消息记录
    async openChat(uid, uname, uavatar) {
      const self = this;
      showLoading()
      self.DialogVisible_2 = true;
      self.chat.id_B = uid;
      self.chat.name_B = uname;
      self.chat.avatar_B = uavatar;
      // 发送请求
      self.$axios({
        method: 'post',
        url: 'api/user/getChatHistory',
        data: {
          id_A: this.chat.id_A,
          id_B: this.chat.id_B
        }
      })
          .then(res => {
            hideLoading()
            switch (res.data.result) {
              case 1:
                console.log("获取聊天历史成功！");
                console.log(res.data.chat_history);
                this.listData = res.data.chat_history
                for(let num = 0;num < this.listData.length; num++){
                  if(this.listData[num].mine){
                    this.listData[num].img = this.chat.avatar_A
                    this.listData[num].name = this.chat.name_A
                  }
                  else{
                    this.listData[num].img = this.chat.avatar_B
                    this.listData[num].name = this.chat.name_B
                  }
                }
                console.log(this.listData)
                break;
              case 0:
                console.log("获取聊天历史失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break
            }
          })
          .catch(err => {
            console.log(err);
          })
    },
    getFriendsList(flag){
      const self = this;
      showLoading()
      self.$axios({
        method: 'post',
        url: 'api/user//getFriendsList',
        data: {
          id: self.iframeData.id
        }
      })
          .then(res => {
            hideLoading()
            switch (res.data.result) {
              case 1:
                console.log("刷新好友列表成功！");
                self.tableData = res.data.friends_list;
                console.log(self.tableData)
                for(let num = 0;num < self.tableData.length;num++){
                  self.tableData[num].profile_photo = require('../../../ExGame-Asset/User/' + self.tableData[num].id +'/ProfilePhoto.jpg')
                  self.tableData[num].status = Boolean(self.tableData[num].status)
                }
                break;
              case 0:
                alert("刷新好友列表失败！");
                this.tableData = []
                break;
              case -1:
                alert("获取数据出现问题！");
                break;
              case -2:
                alert("数据库连接失败！");
                break
            }
          })
          .catch(err => {
            console.log(err);
          })
    }
  },
  mounted() {
    this.getFriendsList(1)
  }
}
</script>

<style scoped>
.Friends{
  margin: 0;
  padding: 0;
}
.top-text{
  margin:10px 50px;
  font-size: 20px;
}
.el-table{
  margin: 0px 0px 30px 50px;
  font-size: small;
}
.el-card{
  margin: 30px;
}
.el-row{
  margin: 30px
}
.add{
  margin: 0 50px
}

</style>