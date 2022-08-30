<!--2053382 范珑骁-->
<template>
  <div class="AccountPage">
    <el-card>
      <div class="top-text">您的信息</div>
      <div class="AccountList">
        <div class="part" id="photo">
          <span class="label-title">头像</span>
          <img class="picture_item" :src= "imgUrl" alt="默认头像" />
          <div id="image">
            <!--上传图片的按钮-->
            <button class="btn" @click="toggleShow">设置头像</button>
            <!--组件主体-->
            <my-upload
                ref="uploadRef"
                field="ProfilePhoto"
                v-model="show"
                @crop-success="cropSuccess"
                :width="300"
                :height="300"
                img-format="jpg"
                :size="size">
            </my-upload>
          </div>
        </div>
        <div class="part" key="id">
          <span class="label-title">账户ID</span>
          <span class="label-content">{{iframeData.id}}</span>
        </div>
        <div class="part" key="email">
          <span class="label-title">电子邮箱</span>
          <span class="label-content">{{form.email}}</span>
        </div>
        <div class="part" key="game-num">
          <span class="label-title">游戏数量</span>
          <span class="label-content">{{form.game_num}}</span>
        </div>
      </div>
    </el-card>
  </div>
</template>

<script>
import 'babel-polyfill'
import myUpload from 'vue-image-crop-upload/upload-2.vue'
import { showLoading, hideLoading } from '@/components/LoadingSet/loading.js';
export default {
  name: "AccountInfo",
  inject: ['updateAvatar'],
  data(){
    return {
      imgUrl: require('../../../ExGame-Asset/User/' + this.$store.state.userID +'/ProfilePhoto.jpg'),
      show: false,  //剪切框显示和隐藏的flag
      size:2.1,
      form:{
        email: '',
        game_num: 0,
      },
      iframeData:{
        id: this.$route.params.id
      }
    }
  },
  //注册组件
  components: {
    "my-upload": myUpload
  },
  methods: {
    //控制剪切框的显示和隐藏
    toggleShow() {
      this.show = !this.show;
    },
    //剪切成功后的回调函数
    cropSuccess(imgDataUrl) {
      //  imgDataUrl其实就是图片的base64data码
      this.imgUrl = imgDataUrl;
      this.updateAvatar(imgDataUrl);
      //这里对base64串进行操作，去掉url头，并转换为byte
      let bytes = window.atob(imgDataUrl.split(',')[1]);
      let array = [];
      for(let i = 0; i < bytes.length; i++){
        array.push(bytes.charCodeAt(i));
      }
      let file = new File([new Uint8Array(array)], 'ProfilePhoto.jpg',{type: 'image/jpeg'});
      let fd = new FormData();
      fd.append('id',this.iframeData.id)
      fd.append('AvatarPicSrc',file);
      for (let [a, b] of fd.entries()) {
        console.log(a, b)
      }
      this.$refs.uploadRef.off();
      this.$axios({
        method: 'post',
        url: 'api/user/uploadAvatar',
        data: fd,
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                console.log("头像上传成功！");
                location.reload()
                break;
              case 0:
                console.log("头像上传失败！");
                break;
            }
          })
          .catch(err => {
            console.log(err);
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
              console.log("获取账户信息成功！");
              self.form.email = res.data.email
              self.form.game_num = res.data.game_num
              self.imgUrl = require('../../../ExGame-Asset/User/'+ self.iframeData.id + '/ProfilePhoto.jpg')
              break;
            case 0:
              console.log("获取账户信息失败！");
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
.AccountPage{
  margin: 0;
  padding: 0;
}
.top-text{
  margin:10px 50px 0;
  font-size: 20px;
}
.el-card{
  margin: 40px 100px;
}
.AccountList{
  margin: 20px 40px;
  text-align: left;
  font-size: 17px;
  vertical-align: middle;
  display: flex;
  flex-direction: column;
  overflow: hidden;
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
  width: 70px;
  height: 40px;
  line-height: 1.8rem;
  font-size: 12px;
  white-space: normal;
  border: thin solid #c8c8c8;
  box-sizing: border-box;
  border-radius: 2px;
  background-color: #fff;
  position: absolute;
  right: 0;
  bottom: 7%;
}
button:hover{
  background-color: #F5F5F5;
}
.picture_item{
  width: 18%;
  height: 18%;
  position: relative;
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
</style>