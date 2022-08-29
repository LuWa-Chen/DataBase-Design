<template>
  <div class = "home-page">

    <!-- 1.header头部 -->
      <game-page-header></game-page-header>
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
    <!-- 2.content主体内容区 -->
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
          <div class="head-text"><font  class="lefttext">发行商名称：{{this.$store.state.userName}}</font>
          </div>
          <div class="line" />
          <div class="head-text"><font  class="lefttext">发行商发行游戏数量：{{iframeData.game_count}}</font>
          </div>
        </div>
        <!--个人信息-->
        <div class="side-choice">
          <li><router-link class="lead-button"  active-class="active" :to="{path:'/selectgame',query:{id:this.iframeData.id,}}" exact>
            <font  class="leftbottomtext">精  选  游  戏</font>
          </router-link></li>
          <li><router-link class="lead-button" active-class="active" :to="{path:'/discountgame',query:{id:this.iframeData.id}}">
            <font  class="leftbottomtext">折  扣  游  戏</font>
          </router-link></li>
          <li><router-link class="lead-button" active-class="active" to="/publishgame">
            <font  class="leftbottomtext">发  行  游  戏</font>
          </router-link></li>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import GamePageHeader from "@/views/GamePageHeader";

export default {
  name: "home-page",
  data(){
    return {
      iframeData:{
        id: this.$store.state.userID,
        games:this.$route.query.games,
        game_count:this.$route.query.game_count,
        result:this.$route.query.result,
        profile_photo: require('../../../ExGame-Asset/Publisher/' + this.$store.state.userID + '/LOGO.jpg')
      }
    }
  },
  components: {
    GamePageHeader,
  },
  methods: {
  },
  created() {
    const self = this;
    self.$axios({
      method: 'post',
      url: 'api/publisher/getgameinfo',
      data: {
        publisher:this.$store.state.userName,
      }
    })
        .then(res => {
          switch (res.data.result) {
            case 1:
              alert("查询成功！");
              break;
            case 0:
              alert("查询失败！");
              break;
            case -1:
              alert("数据库连接失败！");
              break;
          }
        })
        .catch(err => {
          console.log(err);
        })
  }
}
</script>
<style scoped>
#particles-js {
  width: 100%;
  height: calc(100% - 100px);
  position: absolute;
}
.home-page{
  margin: 0px;
  background-color: #121212;
  padding: 0px;
  height:100%;
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
  color: #121212;
  font-size: 22px;
  left: 100px;
  position: absolute;
}
/* 主体 */
.container{
  padding: 0 0 0 350px;
  overflow: hidden;
  margin: 10px 182px 100px;
  border-radius: 30px;
  border: 8px solid #696969;
  background-color: #c8c8c8;
}
.conculor{
  float: left;
  height: 800px;
  position: relative;
}
/*侧边栏*/
.left{
  width: 280px;
  background-color: #F0F0F0;
  margin-left: -100%;
  left: -315px;
  position: relative;
}
.head-portrait{

  width: 30%;
  margin-top: 10px;
  margin-left: 95px;
}
.head-portrait img{
  width: 100%;
  padding: 0px;

  border: 2px solid black;
}
.head-text{
  padding: 25px;
  word-break: break-all;
  color:#121212;
  word-wrap: break-word;
  box-sizing: border-box;
  float: left;
  margin-top: 1px;
  margin-left: 10px;
}
.side-choice{
  width: 100%;
  font-size: 18px;
  font-weight: normal;
  float: left;
  list-style-type: none;
  overflow: auto;
}

li:hover{
  background-color: #E3E7EA;
}
#account{
  margin: 20px 0 25px;
}

.lead-button
{
  padding: 30px;
  display: block;
  color: #000;
  text-align: center;
  text-decoration: none;
  border: 1px solid #000;
  border-right:none;
  border-left:none;
  border-bottom:none;
}


.content{
  width: 100%;
  background-color: #fff;
}
.line{
  float:right;
  width: 100%;
  height: 1px;
  margin-top: 0.5em;
  background:black;
  position: relative;
  text-align: center;
}
.lefttext
{
  font-family: 楷书;
  font-size: 18px;
}
.leftbottomtext
{
  font-family: 楷书;
  font-size: 22px;
  font-weight: bold;
}
</style>