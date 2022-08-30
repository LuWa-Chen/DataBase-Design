<!--2053382 范珑骁-->
<template>
  <div class="editpwd container">
    <form class="form">
      <div class="form-group">
        <label>旧密码</label>
        <input type="text" required placeholder="请填写您原来的密码" autocomplete="off" class="form-password" v-model="password_old"/>
        <label>新密码</label>
        <input type="password" required placeholder="请填写您的新密码" autocomplete="off" class="form-email" v-model="form.password"/>
        <label>确认密码</label>
        <input type="password" required placeholder="请确认您的新密码" autocomplete="off" class="form-email" v-model="password_new"/>
      </div>
      <button type="submit" @click="editPwd" class="btn btn-info">立即提交</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "EditPwd",
  data(){
    return{
      password_old: '',
      password_new: '',
      form: {
        id: this.iframeData.id,
        email: this.iframeData.email,
        password: '',
      }
    };
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
  methods:{
    editPwd(e){
      if(!this.form.password || !this.password_old || !this.password_new){
        this.$layer.msg("请添加对应信息！")
      }else{
        if(this.password_new === this.form.password){
          this.$axios({
            method:'post',
            url: 'api/user/editPassword',
            data: {
              id: this.form.id,
              password_old: this.password_old,
              password: this.form.password
            }
          })
          .then(res => {
          switch (res.data.result) {
            case 1:
              console.log("修改密码成功！");
              this.$layer.close(this.layerid);
              this.$layer.msg("修改密码成功！");
              break;
            case 0:
              console.log("修改密码失败！");
              break;
            case -1:
              alert("传送数据出现问题！");
              break;
            case -2:
              this.$layer.msg("您输入的旧密码有误，请重新输入！");
              break;
          }
          });
        }
        else{
          this.$layer.msg("两次输入新密码不一致，请重新输入！");
        }
        e.preventDefault()
      }
      e.preventDefault()
    }
  }
}
</script>

<style scoped>
.editpwd{
  margin:10px 20px 20px 23px;
}
.form{
  display: flex;
  flex-direction: column;
  vertical-align: middle;
  position: relative;
}
.form-group{
  display: flex;
  flex-direction: column;
  vertical-align: center;
  text-align: left;
  position: relative;
}
input{
  border-color: #c8c8c8;
  background-color: #fff;
  border-radius: 2px;
  border-style: solid;
  border-width: 2px;
  box-sizing: border-box;
  padding: 8px 20px;
  width: 150%;
  height: 150%;
}
button{
  width: 80px;
  height: 40px;
  margin-left: 110px;
  margin-top:40px;
  white-space: normal;
  border: 1px solid #c8c8c8;
  box-sizing: border-box;
  border-radius: 2px;
  background-color: #fff;
}
button:hover{
  background-color: #F5F5F5;
}
label{
  color: rgba(0,0,0,.87);
  line-height: 30px;
  font-size: 15px;
  margin-top: 10px;
  margin-bottom: 5px;
}
</style>