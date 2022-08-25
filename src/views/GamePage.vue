<!--2052535 赵海博-->
<template>
  <div>
  <head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>臻Game</title>
    <link rel="stylesheet" href="https://unpkg.com/swiper@7/swiper-bundle.min.css"/>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Jost:wght@100;200;300;400;500;600;700;800;900&family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../assets/sea_css/index-style.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
  </head>

    <body id="stop-scroll">
<!--    header-->
    <game-page-header></game-page-header>
<!--uerinfo-->
<!--    <user-info style="display: inline-block"></user-info>-->
<!--    左上角搜索栏-->
    <div class="search-bar">
      <i class="fas fa-search search-icon-mobile"></i>

      <div class="search-bar-main">
        <div class="search-div">
          <i class="fas fa-search search-icon-pc" @click="search()"></i>
          <form>
            <input id="searchtext" placeholder="搜索" type="text" autocomplete="off">
          </form>

        </div>
        <select id="choice" style="background-color: #2a2a2a; color: #e0e0e0">
          <option>按游戏名称搜索</option>
          <option>按发行厂商搜索</option>
        </select>
      </div>
    </div>
    <vue-particles
      color="#00FFFF"
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
<!--    首页大swiper-->
    <div class="pc-big-ca">

      <div class="swiper-container mySwiper pc-swiper-f" v-if="swiper_game_name">

        <div class="swiper-wrapper">

          <div class="swiper-slide" v-for="index in swiper_game_name.length" :key="index" >


            <div class="swiper-slide">
              <div class="container">
              <div class="box">
                <span></span>
                <span></span>
                <span></span>
                <span></span>
              <img class="pc-swipe-img" :src="require('../../../ExGame-Asset/Game/' + swiper_cover[index - 1])" style="margin: 0; width: 100%; height: 100%" alt="">

              <div class="text-area">
                <p class="r-date">{{ swiper_publish_date[index - 1] }}</p>
                <p class="game-detal">{{ swiper_general_intro[index - 1] }}</p>
                <div class="price-text">¥{{ swiper_price[index - 1] }}
                </div>
                </div>
              </div>
              </div>
            </div>
        </div>
      </div>
      </div>

      <div thumbsSlider="" class="swiper mySwiper3 pc-swiper-s" v-if="swiper_game_name">
        <div class="swiper-wrapper">
          <div class="swiper-slide" v-for="index in swiper_game_name.length" :key="index" >
            <img :src="require('../../../ExGame-Asset/Game/' + swiper_ancover[index - 1])" width="65" alt="缩略图1"/>
            <p>{{ swiper_game_name[index - 1] }}</p>
          </div>
        </div>
      </div>
    </div>
<!--    正在打折的游戏-->
    <div class="game-sale-pc">
      <div class="main">
        <div class="text-icon">
          <p>正在热销</p>
          <div class="icons">
            <div class="circle" @onclick="slideleft()">
              <i class="fas fa-chevron-left" ></i>
            </div>
            <div class="circle" @onclick="slideright()">
              <i class="fas fa-chevron-right" ></i>
            </div>
          </div>
        </div>
        <div class="cards" v-if="sale_ancover">
          <div class="allcards" id="asd">
            <div class="card" v-for="index in sale_ancover.length" :key="index">
              <img :src="require('../../../ExGame-Asset/Game/' + sale_ancover[index - 1])" alt="">
              <div class="game-detail">
                <h6>{{sale_game_name[index - 1]}}</h6>
                <div>
                  <p class="p-sale-button">-{{100-sale_discount[index-1]*100}}%</p>
                  <p class="sale-price">{{sale_price[index - 1]}}</p>
                  <p>{{numFilter(sale_price[index - 1] * sale_discount[index - 1])}}</p>
                </div>
              </div>
            </div>

          </div>
        </div>
      </div>
    </div>
<!--    游戏列表-->
    <div class="swiper third-swipe">
      <div class="swiper-wrapper">
        <div class="swiper-slide swiper-third">
          <div class="free-text-button">
            <div class="icon-text">
              <h6>新品榜</h6>
            </div>
            <a href="#">查看更多</a>
          </div>
          <div class="car-tuning" v-for="index in newrelease.length" :key="index">
            <img :src="require('../../../ExGame-Asset/Game/' + new_cover[index - 1])" alt="">
            <!--          {{new_cover[index - 1]}}-->
            <div class="game-detail">
              <h6>{{ new_game_name[index - 1] }}</h6>
              <p class="new-epic-button">新品</p>
              <div>
                <p class="p-sale-button">-20%</p>
                <p class="sale-price">{{ new_price[index - 1] }}</p>
                <p>{{ new_price[index - 1] }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="swiper-slide swiper-third">
          <div class="free-text-button">
            <div class="icon-text">
              <h6>热销榜</h6>
            </div>
            <a href="#">查看更多</a>
          </div>
          <div class="car-tuning" v-for="index in hotsale.length" :key="index">
            <img :src="require('../../../ExGame-Asset/Game/' + hot_cover[index - 1])" alt="">
            <div class="game-detail">
              <h6>{{ hot_game_name[index - 1] }}</h6>
              <div>
                <p>{{ hot_price[index - 1] }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="swiper-slide swiper-third">
          <div class="free-text-button">
            <div class="icon-text">
              <h6>好评榜</h6>
            </div>
            <a href="#">查看更多</a>
          </div>
          <div class="car-tuning" v-for="index in praise.length" :key="index">
            <img :src="require('../../../ExGame-Asset/Game/' + praise_cover[index - 1])" alt="">
            <div class="game-detail">
              <h6>{{praise_game_name[index - 1]}}</h6>
              <div>
                <p>{{ praise_price[index - 1] }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!--    <div class="swiper-pagination"></div>-->

    </div>
<!--    底栏-->
    <div class="swiper third-swipe">

      <div class="swiper fourth-swipe">
        <h6 class="rec-text">为你推荐</h6>
        <div class="swiper-wrapper">
          <div class="swiper-slide swiper-fourth" v-for="index in recommend.length" :key="index">
            <img :src="require('../../../ExGame-Asset/Game/' + recommend_cover[index - 1])" alt="">
            <div class="game-detail">
              <h6>{{ recommend_game_name[index - 1] }}</h6>
              <div>
                <p>¥{{recommend_price[index - 1]}}</p>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="swiper fourth-swipe">
        <h6 class="rec-text">即将上架</h6>
        <div class="swiper-wrapper">
          <div class="swiper-slide swiper-fourth" v-for="index in later.length" :key="index">
            <img :src="require('../../../ExGame-Asset/Game/' + later_cover[index - 1])" alt="">
            <div class="game-detail">
              <h6>{{later_game_name[index - 1]}}</h6>
              <div>
                <p class="grey-text">¥{{later_price[index - 1]}}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
<!--    <buttom-list></buttom-list>-->
    </body>
  </div>
</template>


<script>

import GamePageHeader from "@/views/GamePageHeader";
import Swiper from 'swiper'; // 注意引入的是Swiper
// import 'swiper/css/swiper.min.css'
// import 'swiper/swiper-bundle.css'
import 'swiper/dist/css/swiper.min.css'
import 'swiper/dist/js/swiper.min'
// import UserInfo from "@/views/UserInfo";

export default {

  name: 'stop-scroll',
  components: {
    // UserInfo,
    GamePageHeader,

  },
  data(){
    return{
      game_id: "",
      search_choice:0,
      // big swiper
      swiper_game_name: [],
      swiper_publish_date:[],
      swiper_price:[],
      swiper_cover:[],
      swiper_ancover:[],
      swiper_general_intro:[],
      // game sale
      asd_right: "0",
      sale_game_name: [],
      sale_price:[],
      sale_ancover:[],
      sale_discount:[],
      // game list
      newrelease: [], // 列表1的id
      new_game_name:[],
      new_cover:[],
      new_price:[],
      hotsale: [], // 列表2的id
      hot_game_name:[],
      hot_cover:[],
      hot_price:[],
      praise: [], // 列表3的id
      praise_game_name:[],
      praise_cover:[],
      praise_price:[],

      // buttom list
      recommend:[],
      recommend_game_name:[],
      recommend_cover:[],
      recommend_price:[],
      later:[],
      later_game_name:[],
      later_cover:[],
      later_price:[],
    }
  },
  mounted:function() {
    new Swiper('.swiper-container', {
      observer:true,//修改swiper自己或子元素时，自动初始化swiper
      observeParents:true,//修改swiper的父元素时，自动初始化swiper
      autoplay: true,
      loop: false,
      spaceBetween: 30,
      // 其他配置...
    })
  },
  props: {
    msg: String
  },
  methods:{
    ToGame(){
       this.$router.push({name:'GamePage'});
    },
    ToLibrary(){
       this.$router.push({path:'/library'});
    },
    ToHome(){
       this.$router.push({path:'/home'});
    },
    search(){
      let a = document.getElementById("searchtext").value;

      this.search_choice = document.getElementById("choice").value;
      console.log("search_name: " + a);

      this.$router.push({name:'SearchResult', query: {search_name: a, search_choice:this.search_choice}});
    },
    //big swiper
    getSwiperInfo(game_id) {
      const self = this;
      let a = "轮播图";
      let i;
      self.$axios({
        method: 'post',
        url: 'api/getGameInfo',
        data: {
          game_id: game_id
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 0:
                console.log(res.data.result);
                alert("轮播图申请数据失败");
                break;
              case -1:
                alert("轮播图数据库端出现问题，请联系管理人员");
                break;
              case -2:
                alert("数据库出现问题");
                break;
              case 1:
                a = a + game_id.toString() + "加载成功";

                for (i in res.data.game_name) {
                  console.log('get   ' + res.data.game_name[i])
                  this.swiper_game_name.push(res.data.game_name[i]);
                }

                for (i in res.data.publish_date) {
                  console.log('get   ' + res.data.publish_date[i])
                  this.swiper_publish_date.push(res.data.publish_date[i]);
                }

                for (i in res.data.price) {
                  console.log('get   ' + res.data.price[i])
                  this.swiper_price.push(res.data.price[i]);
                }

                for (i in res.data.general_intro) {
                  console.log('get   ' + res.data.general_intro[i])
                  this.swiper_general_intro.push(res.data.general_intro[i]);
                }
                break;

            }
          })
          .catch(err => {
            console.log(err);
          })
    },

    //game sale
    numFilter(value) {
      return parseFloat(value).toFixed(2);
    },
    slideleft(){
      // this.asd.style.right = "0";
      this.asd_right = "0";
    },
    slideright(){
      // this.asd.style.right = "50%";
      this.asd_right = "50%";
    },
    getSaleInfo(game_id){
      const self = this;
      let a = "轮播图";
      let i;
      self.$axios({
        method:'post',
        url: 'api/getGameInfo',
        data: {
          game_id: game_id
        }
      })
          .then(res=>{
            switch (res.data.result){
              case 0:
                alert("轮播图申请数据失败111");
                break;
              case -1:
                alert("轮播图数据库端出现问题，请联系管理人员");
                break;
              case 1:
                a = a + game_id.toString() + "加载成功";

                for(i in res.data.game_name)
                {
                  console.log('get sale ' + res.data.game_name[i])
                  this.sale_game_name.push(res.data.game_name[i]) ;
                }

                for(i in res.data.price)
                {
                  console.log('get sale ' + res.data.price[i])
                  this.sale_price.push(res.data.price[i]) ;
                }

                for(i in res.data.discount)
                {
                  console.log('get sale ' + res.data.discount[i])
                  this.sale_discount.push(res.data.discount[i]) ;
                }

                break;
            }
          })
          .catch( err=>{
            console.log(err);
          })
    },

    //game list
    async getGameRank(rankname, number=4, ranklist, game_name, price, cover){
      const self = this;
      await self.$axios({
        method:'post',
        url: 'api/library/GetGameRank',
        data: {
          rank_name: rankname,
          number: number
        }
      })
          .then(res=>{
            let i;
            switch (res.data.result){
              case 0:
                console.log(rankname + number + "申请失败")
                alert(rankname + "申请数据失败");
                break;
              case 1:
                for(i in res.data.id_list)
                {
                  console.log('get   ' + res.data.id_list[i])
                  ranklist.push(res.data.id_list[i]) ;
                }
                break;
            }
          })

      if( ranklist == null)
        return;

      this.getGameListInfo(ranklist, game_name, price);
      let i;
      for(i of ranklist){
        cover.push(i + '/Cover/ancover.jpg');
      }
    },
    getGameListInfo(game_id, game_name, price){
      const self = this;
      let i;
      self.$axios({
        method:'post',
        url: 'api/getGameInfo',
        data: {
          game_id: game_id
        }
      })
          .then(res=>{
            switch (res.data.result){
              case 0:
                console.log(res.data.result);
                alert("GameList申请数据失败");
                break;
              case -1:
                alert("GameList数据库端出现问题，请联系管理人员");
                break;
              case -2:
                alert("数据库出现问题");
                break;
              case 1:
                for(i in res.data.game_name)
                {
                  console.log('get   ' + res.data.game_name[i])
                  game_name.push(res.data.game_name[i]) ;
                }

                for(i in res.data.price)
                {
                  console.log('get   ' + res.data.price[i])
                  price.push(res.data.price[i]) ;
                }

                break;

            }
          })
          .catch( err=>{
            console.log(err);
          })
    }
  },
  created() {
    // bigswiper取数据
    let carousel_game_id = [];
    for (let i = 1 ; i <= 6; i++){
      let game_id = '000000000' + i.toString();
      carousel_game_id.push(game_id);
      this.swiper_cover.push(game_id + '/Cover/cover.jpg');
      this.swiper_ancover.push(game_id + '/Cover/ancover.jpg')
    }
    this.getSwiperInfo(carousel_game_id);

    // game取数据
    let sale_id = [];
    for (let i = 2 ; i <= 6; i++){
      let game_id = '000000000' + i.toString();
      sale_id.push(game_id);
      this.sale_ancover.push(game_id + '/Cover/ancover.jpg');
    }
    this.getSaleInfo(sale_id);

    // game list取数据
    this.getGameRank("hot", 5, this.hotsale, this.hot_game_name, this.hot_price, this.hot_cover);
    this.getGameRank("new", 5, this.newrelease, this.new_game_name, this.new_price, this.new_cover);
    this.getGameRank("praise", 5, this.praise, this.praise_game_name, this.praise_price, this.praise_cover);

    //buttom list取数据
    this.getGameRank("recommend", 4, this.recommend, this.recommend_game_name, this.recommend_price, this.recommend_cover);
    // this.getGameRank("later", 4, this.later, this.later_game_name, this.later_price, this.later_cover);
  }

}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import "../assets/sea_css/index-style.css";
@import "../assets/sea_css/mixins.css";
@import "../assets/sea_css/cards.css";
@import "../assets/sea_css/variables.css";
#particles-js {
  width: 100%;
  height: calc(320% - 100px);
  position: absolute;
}



</style>
