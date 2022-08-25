<!--2053382 范珑骁-->
<template>
  <div class="editmail container">
    <form class="form">
      <div class="form-group">
        <label>请输入您的密码</label>
        <input type="text" required placeholder="请填写您的密码" autocomplete="off" class="form-password" v-model="password">
        <label>新邮箱</label>
        <input type="text" required placeholder="请填写您的新邮箱" autocomplete="off" class="form-email" v-model="form.email">
      </div>
      <button type="submit" @click="editMail" class="btn btn-info">立即提交</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "EditMail",
  data(){
    return{
      password: '',
      form: this.iframeData
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
    editMail(e){
      if(!this.form.email){
        this.$layer.msg("请添加对应信息！")
      }else{
        this.$axios({
          method:'post',
          url: 'api/user/editEmail',
          data: {
            id: this.form.id,
            email: this.form.email,
            password: this.password
          }
        })
        .then(res => {
        switch (res.data.result) {
          case 1:
            console.log("修改邮箱成功！");
            this.$parent.$data.iframeData = Object.assign({}, this.form);
            this.$layer.close(this.layerid);
            this.$layer.msg("修改邮箱成功！");
            break;
          case 0:
            console.log("修改邮箱失败！");
            this.$layer.msg("您输入的密码有误，请重新输入！");
            break;
          case -1:
            alert("传送数据出现问题！");
            break;
          case -2:
            this.$layer.msg("您输入的密码有误，请重新输入！");
            break
        }
        });
        e.preventDefault()
      }
      e.preventDefault()
    },
    cancel() {
      this.$layer.close(this.layerid);
    }
  }
}
</script>

<style scoped>
.editmail{
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
  -webkit-tap-highlight-color: transparent;
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