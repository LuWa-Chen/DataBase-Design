<template>
  <div class="PublisherProfile">
    <el-card>
      <div class="top-text">发行商信息</div>
      <div class="InfoList">
        <div class="part" id="photo">
          <span class="label-title">Logo</span>
          <img class="picture_item" :src= "imgUrl" alt="默认Logo" />
          <div id="image">
            <!--上传图片的按钮-->
            <button class="btn" @click="toggleShow">上传</button>
            <my-upload
                ref="uploadRef"
                field="LOGO"
                v-model="show"
                @crop-success="cropSuccess"
                :width="300"
                :height="300"
                img-format="jpg"
                :size="size"
            >
            </my-upload>
          </div>
        </div>
        <div class="part" key="id">
          <span class="label-title">发行商ID</span>
          <span class="label-content">{{iframeData.id}}</span>
        </div>
        <div class="part" key="name">
          <span class="label-title">名称</span>
          <span class="label-content">{{iframeData.name}}</span>
          <div class="edit-button">
            <button class="layui-btn  layui-btn-normal  layui-btn-sm" @click="edit()">
              编辑
            </button>
          </div>
        </div>
        <div class="part" key="phone_number">
          <span class="label-title">手机号</span>
          <span class="label-content">{{iframeData.phone_number}}</span>
          <div class="edit-button">
            <button class="layui-btn  layui-btn-normal  layui-btn-sm" @click="edit()">
              编辑
            </button>
          </div>
        </div>
        <div class="part" key="area">
          <span class="label-title">地区</span>
          <span class="label-content">{{iframeData.area}}</span>
        </div>
        <div class="part" key="email">
          <span class="label-title">电子邮箱</span>
          <span class="label-content">{{iframeData.email}}</span>
        </div>

      </div>
    </el-card>
  </div>
</template>


<script>
import 'babel-polyfill'
import myUpload from 'vue-image-crop-upload/upload-2.vue'
import { showLoading, hideLoading } from '@/components/LoadingSet/loading.js';
import EditPublisher from './EditPublisher';
export default {
  name: "PublisherProfile",
  data(){
    return {
      imgUrl: require('../../../ExGame-Asset/Publisher/' + this.$store.state.userID + '/LOGO.jpg'),
      show: false,  //剪切框显示和隐藏的flag
      size:2.1,
      iframeData:{
        id: this.$store.state.userID,
        name: this.$store.state.userName,
        email: '2052733@tongji.edu.cn',
        phone_number: '19956737576',
        area: '中国-上海'
      }
    }
  },
  //注册组件
  components: {
    "my-upload": myUpload,
    EditPublisher
  },
  methods: {
    edit() {
      this.$layer.iframe({
        type: 2,
        title: "发行商",
        area: ['360px', '310px'],
        shade: true,
        offset: 'auto',
        content: {
          content: EditPublisher,//传递的编辑组件主线
          parent: this,
          data: {
            iframeData:this.iframeData
          }
        }
      })
    },
    //控制剪切框的显示和隐藏
    toggleShow() {
      this.show = !this.show;
    },
    //剪切成功后的回调函数
    cropSuccess(imgDataUrl) {
      //  imgDataUrl其实就是图片的base64data码
      this.imgUrl = imgDataUrl;
      //这里对base64串进行操作，去掉url头，并转换为byte
      let bytes = window.atob(imgDataUrl.split(',')[1]);
      let array = [];
      for(let i = 0; i < bytes.length; i++){
        array.push(bytes.charCodeAt(i));
      }
      let file = new File([new Uint8Array(array)], 'LOGO.jpg',{type: 'image/jpeg'});
      let fd = new FormData();
      fd.append('id',this.iframeData.id)
      fd.append('logo',file);
      for (let [a, b] of fd.entries()) {
        console.log(a, b)
      }
      this.$refs.uploadRef.off();
      this.$axios({
        method: 'post',
        url: 'api/publisher/editProfile',
        data: fd,
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                console.log("Logo上传成功！");
                break;
              case 0:
                console.log("Logo上传失败！");
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
      url: 'api/publisher/getpubinfo',
      data: {
        id: self.iframeData.id
      }
    })
        .then(res => {
          hideLoading()
          switch (res.data.result) {
            case 1:
              console.log("获取账户信息成功！");
              self.iframeData.email = res.data.email
              self.iframeData.phone_number = res.data.phone_number
              self.iframeData.area = res.data.area
              self.imgUrl = require('../../../ExGame-Asset/Publisher/'+ self.iframeData.id + '/LOGO.jpg')
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
.top-text{
  margin:10px 50px 0;
  font-size: 20px;
}
.el-card{
  margin: 40px 100px;
}
.InfoList{
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
  width: 55px;
  height: 40px;
  line-height: 1rem;
  font-size: 12px;
  white-space: normal;
  border: thin solid #c8c8c8;
  box-sizing: border-box;
  border-radius: 2px;
  background-color: #fff;
  position: absolute;
  right: 0;
}
#uploadLogo{
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