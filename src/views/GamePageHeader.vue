<template>
  <header>
    <div class="navbar">
      <div class="left-sec">
<!--        <img src="../assets/sea_image/epic logo.png" alt="">-->
        <ul class="pc-navbar-list">
          <li><router-link :to="{name:'GamePage'}"><a id="active" href="#">臻Game</a></router-link></li>
          <li><a href="#">FAQ & 帮助</a></li>
          <li><a href="#">加入我们</a></li>
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
          <i class="fas fa-user"></i>
          <p>{{this.$store.state.userName}}</p>
          <div class="userinfo" v-if="is_show_userinfo">
            <div class="title">ID:{{this.$store.state.userID}}</div>
              <div class="swiper third-swipe">
                <div class="swiper-wrapper">
                  <div class="swiper-slide swiper-third">
                    <div class="car-tuning" @click="ToAccount">
                      <div class="game-detail">
                        <h6>账户信息</h6>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToProfile">
                      <div class="game-detail">
                        <h6>个人简介</h6>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToSafety">
                      <div class="game-detail">
                        <h6>安全设置</h6>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToFriends">
                      <div class="game-detail">
                        <h6>好友列表</h6>
                      </div>
                    </div>
                    <div class="car-tuning" @click="ToLibrary">
                      <div class="game-detail">
                        <h6>已购游戏</h6>
                      </div>
                    </div>
                    <div class="car-tuning" @click="Logout">
                      <div class="game-detail">
                        <h6> 登出 </h6>
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
  			profile_photo: require('../../../ExGame-Asset/User/' + this.$store.state.userID +'/ProfilePhoto.jpg')

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
      Logout(){
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
</script>

<style scoped>
@import "../assets/sea_css/index-style.css";

/* 购物车 */
.header-area .uer-info {
  position: absolute;
  /*float: right;*/
  top: 10px;
  right: 120px;
  display: inline-block;
  border: 1px solid #999;
  height: 34px;
  width: 80px;
  line-height: 34px;
  /*padding: 0 20px;*/

}
.header-area .uer-info .user-info-header {
  position: absolute;
  top: 0.5px;
  right: 0.5px;
  width: 80px;
  height: 30px;
  /*background-color: red;*/
  /*border: 1px solid #999;*/
}
.header-area .uer-info:hover .user-info-header {
  background-color: #fff;
  border-bottom: 1px solid #fff;
  z-index: 20;
}
.header-area .uer-info .user-info-header span:first-child {
  margin-left: 10px;
  color: #129934;
}
.header-area .uer-info .shopping-cart-icon {
  display: inline-block;
  width: 22.2px;
  height: 19.2px;
  /*background-color: red;*/
  /*margin-top: */
  position: absolute;
  top: 5px;
  right: 20px;
  /*background:  url('../../assets/img/app-header/icons/shopping-cart.png') no-repeat;*/
  background-size: 22.2px 19.2px;
  background-position: 0 0;
}
.header-area .uer-info .shopping-cart-number {
  display: inline-block;
  width: 20px;
  height: 20px;
  line-height: 20px;
  text-align: center;
  /*background-color: red;*/
  font-size: 10px;
  font-weight: bold;
  color: #12ab34;
  position: absolute;
  top: 0px;
  right: 19px;
}
.header-area .uer-info .user-info-list {
  /*box-sizing: border-box;*/
  position: absolute;
  top: 30px;
  right: -1px;
  width: 460px;
  background-color: #fff;
  border: 1px solid #999;
  font-size: 14px;
  z-index: 10;
  /*background-color: red;*/
}
.header-area .uer-info .user-info-list ul {
  min-height: 100px;
  max-height: 320px;
  overflow: auto;
}
.header-area .uer-info .user-info-list .product-item {
  box-sizing: border-box;
  height: 80px;
  /*width: */
  padding: 5px;
  border-bottom: 1px solid #eee;
  position: relative;
}
.header-area .uer-info .user-info-list .product-item:hover {
  background-color: #eee;
}
.header-area .uer-info .user-info-list .product-item img {
  box-sizing: border-box;
  width: 70px;
  height: 70px;
  border: 1px solid #eee;
}
.header-area .uer-info .user-info-list .product-item .product-info {
  position: absolute;
  top: 5px;
  right: 5px;
  margin: 5px 95px;
  /*margin-bottom: 5px;*/
  height: 60px;
  line-height: 20px;
  /*background-color: red;*/
  overflow: hidden;
}
.header-area .uer-info .user-info-list .product-item .product-number {
  position: absolute;
  top: 10px;
  right: 10px;
  height: 20px;
  line-height: 20px;
  /*background-color: red;*/
}
.header-area .uer-info .user-info-list .product-item .product-number .price {
  color: red;
}
.header-area .uer-info .user-info-list .product-item .delete-product {
  float: right;
  margin-right: 10px;
  /*font-size: 14px;*/
}
.header-area .uer-info .user-info-list .product-item .delete-product:hover {
  cursor: pointer;
  color: red;

}
.header-area .uer-info .user-info-list .total {
  padding: 0 20px;
  height: 40px;
  line-height: 40px;
  background-color: #eee;
  /*position: absolute;*/
  bottom: 0;
  left: 0;
}
.header-area .uer-info .user-info-list .total span {
  margin: 0 10px;
}
.header-area .uer-info .user-info-list .total span:nth-child(3) {
  float: right;
  display: inline-block;
  background-color: #12ab34;
  height: 20px;
  line-height: 20px;
  padding: 5px 10px;
  /*padding: 5px;*/
  color: #fff;
  font-weight: bold;
  border-radius: 2px;
  margin: 5px 20px;
}
.header-area .uer-info .user-info-list .total span:nth-child(3):hover {
  cursor: pointer;
}
.header-area .uer-info .user-info-list .has-nothing {
  text-align: center;
  margin-top: 35px;
  /*background-color: red;*/
}
</style>
