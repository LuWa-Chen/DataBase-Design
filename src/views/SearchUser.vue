<!--2053382 范珑骁-->
<template>
  <div class="search">
    <el-row :gutter="20">
      <el-col :span="15">
        <el-input
            placeholder="请输入要查找的用户的id"
            v-model="queryInfo.id"
            clearable
            @clear="this.tableData.length = 0"
        >
          <el-button
              slot="append"
              icon="el-icon-search"
              @click="getUsersList(queryInfo.id, 0)"
          ></el-button>
        </el-input>
      </el-col>
    </el-row>
    <el-table
        :data="tableData"
        border
        style="width: 91%"
        :header-cell-style="{
          'text-align': 'center',
          background: '#F5F5F5'
        }"
        :cell-style="{
          'text-align': 'center'
        }"
        max-height="500"
        empty-text="抱歉，没有查找到该用户~~~">
      <el-table-column
          fixed
          prop="id"
          label="用户id"
          width="140">
      </el-table-column>
      <el-table-column
          prop="profile_photo"
          label="头像"
          width="120">
        <template slot-scope="scope">
          <img :src="scope.row.profile_photo" width="60" height="60" @error="defImg"/>
        </template>
      </el-table-column>
      <el-table-column
          prop="name"
          label="昵称"
          width="130">
      </el-table-column>
      <el-table-column
          fixed="right"
          label="操作"
          width="180">
        <!-- 查看按钮 -->
        <template slot-scope="scope">
          <el-button
              type="primary"
              icon="el-icon-tickets"
              size="mini"
              @click="getUsersList(scope.row.id, 1)"
          >查看</el-button>
          <!-- 添加按钮 -->
          <el-button
              type="danger"
              icon="el-icon-plus"
              size="mini"
              @click="addUserById(scope.row.id)"
          >添加</el-button>
        </template>
      </el-table-column>
    </el-table>
    <!--查看用户详细资料对话框-->
    <InfoDialog :form="form" :DialogVisible="DialogVisible" @callBack="callBack"></InfoDialog>
  </div>
</template>

<script>
import InfoDialog from './InfoDialog';
export default {
  name: "SearchUser",
  data(){
    return{
      DialogVisible: false,
      defImg: require("../assets/avatar.jpg"),
      queryInfo:{
        id: ''
      },
      form:{
        profile_photo:'',
        id:'',
        name:'',
        email:'',
        birthday:'',
        game_num: 0,
        intro:''
      },
      tableData:[],
      tableData_2:[
        {
          id: '',
          name:'',
          profile_photo: ''
        }
      ]
    }
  },
  props:{
    layerid: {
      type: String,
      default: ""
    },
    iframeData: {//传递的数据
      type: Object,
      default: () => {
        return {};
      }
    },
    lydata: {
      type: Object,
      default: () => {
        return {};
      }
    }
  },
  components:{
    InfoDialog
  },
  methods:{
    callBack(flag){
      this.DialogVisible = flag
    },
    getUsersList(uid, flag) {
      const self = this;
      if(flag === 1)
        self.DialogVisible = true;
      self.$axios({
        method: 'post',
        url: 'api/user/getUserInfo',
        data: {
          id: uid
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                console.log("查找成功！");
                self.tableData_2[0].id = uid;
                self.tableData_2[0].name = res.data.name;
                self.tableData_2[0].profile_photo = require('../../../ExGame-Asset/User/' + uid +'/ProfilePhoto.jpg');
                self.tableData = self.tableData_2
                this.tableData.splice(1.0)//刷新表格视图
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
                self.tableData = [];
                this.tableData.splice(1.0)//刷新表格视图
                this.$layer.msg("不存在该用户！");
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
    },
    async addUserById(uid){
      // 弹框提示用户是否继续添加
      const confirmResult = await this.$confirm(
          '确定添加该用户为好友吗?',
          '提示',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'info'
          }
      ).catch(err => err)
      // 如果用户确认添加，则返回字符串 confirm
      // 如果用户取消添加，则返回字符串 cancel
      if (confirmResult !== 'confirm') {
        return this.$message.info('已取消添加')
      }
      // 发送添加请求
      this.$axios({
        method: 'post',
        url: 'api/user/addFriends',
        data: {
          id_A: this.iframeData.id,
          id_B: uid
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                console.log("好友添加成功！");
                this.$layer.msg("好友添加成功！");
                break;
              case 0:
                console.log("好友添加失败！");
                this.$layer.msg("好友添加失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break
            }
          })
          .catch(err => {
            console.log(err);
          })

    }
  },
  created(){
    this.tableData = []
  }
}
</script>

<style scoped>
.el-table{
  margin: 30px 40px;
}
.el-col{
  margin: 30px 40px 20px;
}
.noChart{
  color:#c8c8c8
}
</style>