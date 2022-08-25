<!--2053382 范珑骁-->
<template>
  <div class = "home-page">
    <!-- 1.header头部 -->
    <game-page-header></game-page-header>
    <!-- 2.content主体内容区 -->
    <vue-particles
      color="#409EFF"
      :particleOpacity="0.7"
      :particlesNumber="50"
      shapeType="star"
      :particleSize="6"
      linesColor="#409EFF"
      :linesWidth="1"
      :lineLinked="true"
      :lineOpacity="0.4"
      :linesDistance="100"
      :moveSpeed="1"
      :hoverEffect="true"
      hoverMode="grab"
      :clickEffect="true"
      clickMode="repulse">
    </vue-particles>
    <div class="container">
      <div class="conculor content">
        <router-view></router-view>
      </div>
      <!-- 侧边栏 -->
      <div class="conculor left">
        <!-- 头像和id、状态 -->
        <div class="top-item">
          <div class="head-portrait">
            <img :src="this.iframeData.profile_photo" alt="默认头像" />
          </div>
          <div class="head-text">ID：{{iframeData.id}}<br/>状态：
            <span v-if="form.status===0">离线</span>
            <span v-if="form.status===1">在线</span>
            <span v-if="form.status===2">隐身</span>
            <span v-if="form.status===3">请勿打扰</span>
          </div>
          <div style='clear:both;height:0;overflow:hidden;'></div>
        </div>
        <!--个人信息-->
        <div class="side-choice">
          <li><router-link class="lead-button" id="account" active-class="active" :to="{name:'account',params:{id:this.iframeData.id}}" exact>
            <span class="iconfont icon-zhanghu"></span>账户信息
          </router-link></li>
          <li><router-link class="lead-button" active-class="active" :to="{name:'profile',params:{id:this.iframeData.id}}">
            <span class="iconfont icon-jianjie"></span>个人简介
          </router-link></li>
          <li><router-link class="lead-button" active-class="active" :to="{name:'safety',params:{id:this.iframeData.id}}">
            <span class="iconfont icon-lock-full"></span>安全设置
          </router-link></li>
          <li><router-link class="lead-button" active-class="active" :to="{name:'friends',params:{id:this.iframeData.id,name:this.iframeData.name,avatar:this.iframeData.profile_photo}}">
            <span class="iconfont icon-haoyou"></span>好友列表
          </router-link></li>
        </div>
        <button class="logout" @click="logout">登出</button>
      </div>
    </div>
  </div>
</template>

<script>
import GamePageHeader from "@/views/GamePageHeader";
export default {
  name: "home-page",
  components: {
      GamePageHeader
    },
  provide(){
    return{
      updateAvatar: this.updateAvatar
    }
  },
  data(){
    return {
      form:{
        status: 1,
      },
      iframeData:{
        id: this.$store.state.userID,
        name:this.$store.state.userName,
        profile_photo: require('../../../ExGame-Asset/User/' + this.$store.state.userID +'/ProfilePhoto.jpg')
      }
    }
  },
  methods: {
    updateAvatar(ImgUrl){
      this.iframeData.profile_photo = ImgUrl
    },
    logout(){
      const self = this;
      self.$axios({
        method:'post',
        url: 'api/user/logout',
        data: {
          id:self.iframeData.id
        }
      })
          .then( res => {
            switch(res.data.result){
              case 1:
                alert("登出成功！");
                self.form.status = 0
                this.$store.commit('changeValue_Name','');
                this.$store.commit('changeValue_ID','');
                this.$router.push({name:'GamePage'})
                break;
              case 0:
                alert("登出失败！");
                break;
            }
          })
          .catch( err => {
            console.log(err);
          })
    }
  },
  mounted() {
    const self = this;
    self.$axios({
      method:'post',
      url: 'api/user/getUserInfo',
      data: {
        id:self.iframeData.id
      }
    })
        .then( res => {
          switch(res.data.result){
            case 1:
              alert("信息初始化成功！");
              break;
            case 0:
              alert("信息初始化失败！");
              break;
            case -1:
              alert("获取数据出现问题！");
              break;
            case -2:
              alert("数据库连接失败！");
              break
          }
          self.form.status = res.data.status
          self.iframeData.name = res.data.name
          self.iframeData.profile_photo = require('../../../ExGame-Asset/User/' + self.iframeData.id +'/ProfilePhoto.jpg')
        })
        .catch( err => {
          console.log(err);
        })
    this.$router.push({name:'account',params:{id:this.iframeData.id}})
  }
}
</script>

<style scoped>
@import "../assets/sea_css/index-style.css";
@import "../assets/sea_css/mixins.css";
@import "../assets/sea_css/cards.css";
@import "../assets/sea_css/variables.css";
#particles-js {
  width: 100%;
  height: calc(100% - 100px);
  position: absolute;
}

.home-page{
  padding: 0;
  background-color: #121212;
  height: 1080px;
}
body{
  min-width: 650px;
  position: fixed;
}
/* 头部 */
.header{
  width: 100%;
  height: 50px;
  line-height: 50px;
  background-color: #000000;
  text-align: center;
  font-weight: bold;
  max-width: 104rem;
  min-width: 76rem;
  position: relative;
}
.header-text{
  color: #FFFFFF;
  font-size: 22px;
  left: 100px;
  position: absolute;
}
/* 主体 */
.container{
  padding: 0 0 0 300px;
  overflow: hidden;
  margin: 10px 200px 100px;
  border-radius: 30px;
  border: 8px solid #696969;
  background-color: #c8c8c8;
}
.conculor{
  float: left;
  position: relative;
}
/*侧边栏*/
.left{
  width: 300px;
  background-color: #F0F0F0;
  margin-left: -100%;
  left: -300px;
  position: relative;
}
.head-portrait{
  margin-top: 10px;
  float: left;
  width: 30%;
}
.head-portrait img{
  width: 75%;
  padding: 20px 0 20px 38px;
}
.head-text{
  padding: 50px 0 0 30px;
  word-break: break-all;
  font-style: italic;
  word-wrap: break-word;
  box-sizing: border-box;
  float: left;
  color: #121212;
}
.side-choice{
  width: 100%;
  font-size: 18px;
  font-weight: normal;
  float: left;
  list-style-type: none;
  overflow: auto;
}
#account{
  margin-top: 10px;
}
.active{
  background-color: #DCDCDC;
}
li:hover{
  background-color: #E3E7EA;
}
.lead-button{
  margin: 0;
  padding: 33px;
  display: block;
  color: #000;
  text-align: center;
  text-decoration: none;
  border-bottom: 5px solid;
  border-bottom-color: #F0F0F0;
}

.logout{
  min-width: 60px;
  margin: 31px 110px;
  width: 30%;
  height: 40px;
  outline: none;
  color: rgba(0,0,0,.87);
  font-size: 0.9em;
  cursor: pointer;
  line-height: 1rem;
  white-space: normal;
  border: thin solid #c8c8c8;
  box-sizing: border-box;
  border-radius: 2px;
  background-color: #fff;
}
.logout:hover{
  background-color: #F5F5F5;
}
.content{
  width: 100%;
}
</style>