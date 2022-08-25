<!--2053382 范珑骁-->
<template>
  <div class="editprofile container">
    <form class="form">
      <div class="form-group">
        <label>昵称</label>
        <input type="text" required placeholder="请填写您的新昵称" autocomplete="off" class="form-name" v-model="form.name" >
        <label>生日</label>
        <input type="date" max="2021-12-31" min="1979-12-31" required placeholder="请填写您的生日" autocomplete="off" class="form-birthday" v-model="form.birthday">
        <label>地区</label>
        <input type="text" required placeholder="请填写您所在的地区" autocomplete="off" class="form-area" v-model="form.area">
        <label>自我介绍</label>
        <textarea required placeholder="请填写您的自我介绍" autocomplete="off" class="form-intro" v-model="form.intro"></textarea>
      </div>
      <button type="submit" @click="editProfile" class="btn btn-info">立即提交</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "EditProfile",
  data(){
    return{
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
    editProfile(e){
      if(!this.form.name || !this.form.birthday || !this.form.intro || !this.form.area){
        this.$layer.msg("请添加对应信息！")
      }else{
        this.$axios({
          method:'post',
          url: 'api/user/editProfile',
          data: {
            id: this.form.id,
            name: this.form.name,
            birthday: this.form.birthday,
            area: this.form.area,
            intro: this.form.intro
          }
        })
        .then(res => {
        switch (res.data.result) {
          case 1:
            console.log("修改个人信息成功！");
            this.$parent.$data.iframeData = Object.assign({}, this.form);
            this.$layer.close(this.layerid);
            this.$layer.msg("修改个人信息成功！");
            break;
          case 0:
            console.log("修改个人信息失败！");
            break;
          case -1:
            alert("传送数据出现问题！");
            break;
          case -2:
            alert("数据库连接失败！");
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
.editprofile{
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
.form-intro{
  padding: 8px 20px 60px 20px;
  border-color: #c8c8c8;
  background-color: #fff;
  border-radius: 2px;
  border-style: solid;
  border-width: 2px;
  box-sizing: border-box;
  -webkit-tap-highlight-color: transparent;
  width: 150%;
  height: 150%;
}
button{
  width: 80px;
  height: 40px;
  margin-left: 110px;
  margin-top:25px;
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