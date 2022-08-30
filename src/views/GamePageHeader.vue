<template>
  <header>
    <link rel="stylesheet" href="https://unpkg.com/swiper@7/swiper-bundle.min.css"/>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Jost:wght@100;200;300;400;500;600;700;800;900&family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../assets/sea_css/index-style.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <div class="navbar">
      <div class="left-sec">
<!--        <img src="../assets/sea_image/epic logo.png" alt="">-->
        <ul class="pc-navbar-list">
          <router-link :to="{name:'GamePage'}"><li><a id="active" href="#">臻Game</a></li></router-link>
          <li><a href="#">FAQ & 帮助</a></li>
          <router-link :to="{name:'P_Register' }" v-if="this.$store.state.isUser!='Publisher'"><li><a href="#">加入我们</a></li></router-link>
        </ul>
      </div>
<!--      <div class="hamburger-menu" id="x-menu" onclick="openright()">-->
<!--        <div class="line" id="tr-right"></div>-->
<!--        <div class="line" id="hide"></div>-->
<!--        <div class="line" id="tr-left"></div>-->
<!--      </div>-->
      <div class="right-sec">
        <i class="fas fa-globe"></i>
        <div class="sign" @click="ToLogin" v-if="this.$store.state.userID==''">
            <i class="fas fa-user"></i>
            <p>登录</p>
        </div>
        <div class="sign" @mouseenter="is_show_userinfo=true" @mouseleave="is_show_userinfo=false" v-else>
          <img :src="this.profile_photo" style="width:40px;height:40px;"/>
          <p>{{this.$store.state.userName}}</p>
          <div class="userinfo" v-if="is_show_userinfo">
            <div class="title">ID:{{this.$store.state.userID}}</div>
              <div class="swiper third-swipe">
                <div class="swiper-wrapper">
                  <div class="swiper-slide swiper-third">
                    <div class="car-tuning" @click="ToPublisherPage" v-if="this.$store.state.isUser=='Publisher'">
                      <div class="game-detail">
                        <h66>形象主页</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToLibrary" v-else>
                      <div class="game-detail">
                        <h66>已购游戏</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToAccount">
                      <div class="game-detail">
                        <h66>账户信息</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToProfile">
                      <div class="game-detail">
                        <h66>个人简介</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToSafety">
                      <div class="game-detail">
                        <h66>安全设置</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToFriends">
                      <div class="game-detail">
                        <h66>好友列表</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToMyOrder">
                      <div class="game-detail">
                        <h66>我的订单</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToCDKey">
                      <div class="game-detail">
                        <h66>CDKey兑换</h66>
                      </div>
                    </div>
                    <div class="car-tuning" @click="Logout">
                      <div class="game-detail">
                        <h66> 登出 </h66>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <a href="#">客户端下载</a>
         </div>

    </div>
  </header>
</template>

<script>
export default {
  name: "GamePageHeader",
  data(){
  		return {
  			userName:this.$store.state.userName,
  			userID:this.$store.state.userID,
  			is_show_userinfo:false,
  			profile_photo: require('../../../ExGame-Asset/' + this.$store.state.isUser +'/' + this.$store.state.userID + '/' + this.$store.state.logo_type)

  		}
  	},
  methods:{
      ToLogin(){
          this.$router.push({name:'Login'});
      },
      ToAccount(){
          this.$router.push({name:'account',params: {id:this.userID}})
      },
      ToProfile(){
          this.$router.push({name:'profile',params: {id:this.userID}})
      },
      ToSafety(){
          this.$router.push({name:'safety',params: {id:this.userID}})
      },
      ToFriends(){
          this.$router.push({name:'friends',params: {id:this.userID,name:this.userName,avatar:this.profile_photo}})
      },
      ToLibrary(){
          this.$router.push({name:'Library'});
      },
      ToMyOrder(){
          this.$router.push({name:'myOrder'});
      },
      ToCDKey(){
          this.$router.push({name:'CDKey'});
      },
      ToPublisherPage(){
          this.$router.push({name:'PublisherPage'});
      },
      Logout(){
          if(this.$store.state.isUser == 'Publisher'){
             this.$store.commit('changeValue_Name','');
             this.$store.commit('changeValue_ID','');
             this.$store.commit('change_flag','');
             this.$router.to(0);
             this.$router.push({name:'GamePage'})
          }
          else{
            const self = this;
               self.$axios({
                 method:'post',
                 url: 'api/user/logout',
                 data: {
                   id:self.userID
                 }
               })
               .then( res => {
                 switch(res.data.result){
                   case 1:
                     alert("登出成功！");
                     this.$store.commit('changeValue_Name','');
                     this.$store.commit('changeValue_ID','');
                     this.$router.to(0);
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
      }
    }
}
</script>

<style scoped>
@import "../assets/sea_css/index-style.css";

</style>
