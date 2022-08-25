<!--2053382 范珑骁-->
<template>
  <div class="Safety">
    <el-card>
      <div class="top-text">安全</div>
      <div class="explainList">
        <div class="part">
          <span class="label-title">邮箱</span>
          <span class="label-content">{{iframeData.email}}</span>
          <div class="edit-button">
            <button class="layui-btn  layui-btn-normal  layui-btn-sm" @click="editmail()">
              修改
            </button>
          </div>
        </div>
        <div class="part">
          <span class="label-title">密码</span>
          <span class="label-content">{{this.star}}</span>
          <div class="edit-button">
            <button class="layui-btn  layui-btn-normal  layui-btn-sm" @click="editpwd()">
              修改
            </button>
          </div>
        </div>
      </div>
    </el-card>
  </div>
</template>

<script>
import EditMail from './EditMail';
import EditPwd from './EditPwd';
import { showLoading, hideLoading } from '@/components/LoadingSet/loading.js';
export default {
  name: "SafetySet",
  data() {
    return {
      star: '',
      iframeData: {
        id: this.$route.params.id,
        email:'',

      }
    }
  },
  components: {
    EditMail,
    EditPwd
  },
  methods: {
    editmail() {
      this.$layer.iframe({
        type: 2,
        title: "修改邮箱",
        area: ['360px', '320px'],
        shade: true,
        offset: 'auto',
        content: {
          content: EditMail,//传递的编辑组件主线
          parent: this,
          data: {
            iframeData: this.iframeData
          }
        }
      })
    },
    editpwd() {
      this.$layer.iframe({
        type: 2,
        title: "修改密码",
        area: ['360px', '400px'],
        shade: true,
        offset: 'auto',
        content: {
          content: EditPwd,//传递的编辑组件主线
          parent: this,
          data: {
            iframeData: this.iframeData
          }
        }
      })
    }
  },
  mounted() {
    const self = this;
    showLoading()
    self.$axios({
      method: 'post',
      url: 'api/user/getUserInfo',
      data: {
        id: self.iframeData.id
      }
    })
        .then(res => {
          hideLoading()
          switch (res.data.result) {
            case 1:
              console.log("获取安全信息成功！");
              self.iframeData.email = res.data.email;
              // 循环遍历拿到密钥的长度
              for (let i = 0; i < 10; i++) {
                let star = self.star.split('') //分割成字符串数组
                star.splice(i, i, '•') //添加到数组
                self.star = star.join('') //将数组转换为字符串
              }
              break;
            case 0:
              console.log("获取安全信息失败！");
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
}
</script>

<style scoped>
.top-text{
  margin:10px 50px 0;
  font-size: 20px;
}
.el-card{
  margin: 40px 100px;
}
.explainList{
  margin: 20px 40px;
  text-align: left;
  font-size: 17px;
  vertical-align: middle;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}
.part{
  padding: 20px;
  margin: 10px;
  width: 90%;
  border-bottom: .1rem solid;
  border-bottom-color: #e6e6e6;
  display: flex;
  align-content: center;
  position: relative;
}
.label-title{
  max-width: 100%;
  width: 35%;
  line-height: 1.8rem;
  vertical-align: middle;
}
.label-content{
  font-weight: 700;
}
button{
  width: 55px;
  height: 40px;
  line-height: 1rem;
  white-space: normal;
  font-size: 12px;
  border: thin solid #c8c8c8;
  box-sizing: border-box;
  border-radius: 2px;
  background-color: #fff;
  position: absolute;
  right: 0;
}
button:hover{
  background-color: #F5F5F5;
}
</style>