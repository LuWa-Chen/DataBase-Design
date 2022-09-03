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
          <form onsubmit="return false;">
            <input id="searchtext" placeholder="搜索"  style="color:white" type="text" autocomplete="off"  @keydown.enter="search()">
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

            <!--              <div class="container">-->
            <div class="box">
              <span></span>
              <span></span>
              <span></span>
              <span></span>
              <img class="pc-swipe-img" :src="require('../../../ExGame-Asset/Game/' + swiper_cover[index - 1])" style="margin: 0; width: 100%; height: 100%" alt="">

              <div class="text-area">
                <p class="r-date">{{ swiper_publish_date[index - 1] }}</p>
                <p class="game-detal" style="width: 300px">{{ swiper_general_intro[index - 1] }}</p>
                <div class="price-text">¥{{ swiper_price[index - 1] }}
                </div>
              </div>
              <!--              </div>-->
            </div>
          </div>
        </div>
      </div>

      <div thumbsSlider="" class="swiper mySwiper3 pc-swiper-s" v-if="swiper_game_name">
        <div class="swiper-wrapper">
          <!--          <div class="swiper-slide">-->
          <div class="swiper-slide" v-for="index in swiper_game_name.length" :key="index" @click="jumpGame(swiper_game_id[index - 1])">

            <div class="container">
              <div class="box">
                <!--                <span></span>-->
                <!--                <span></span>-->
                <!--                <span></span>-->
                <!--                <span></span>-->
                <img :src="require('../../../ExGame-Asset/Game/' + swiper_ancover[index - 1])" width="65" alt="缩略图1">
                <p>{{ swiper_game_name[index - 1] }}</p>
              </div>
            </div>
            <!--              </div>-->

          </div>
        </div>
      </div>
    </div>

    <!--    正在打折的游戏-->
    <div class="game-sale-pc">
      <div class="main">
        <div class="text-icon">
          <p>正在热销</p>
        </div>
        <div class="cards" v-if="sale_ancover">
          <div class="allcards" id="asd">
            <div class="card" v-for="index in sale_game_name.length" :key="index" style="cursor: pointer;" @click="jumpGame(sale_game_id[index - 1])">

              <div class="container" >
                <div class="box" >

                  <img :src="require('../../../ExGame-Asset/Game/' + sale_ancover[index - 1])" alt="" style="border-radius:5px;">
                  <div class="game-detail" >
                    <h6>{{sale_game_name[index - 1]}}</h6>
                    <div>
                      <p class="p-sale-button" v-if="sale_discount[index-1]<1">-{{100-sale_discount[index-1]*100}}%</p>
                      <p class="sale-price-" v-if="sale_discount[index-1]<1">{{sale_price[index - 1]}}</p>
                      <p style="font-size: 15px">¥{{numFilter(sale_price[index - 1] * sale_discount[index - 1])}}</p>
                    </div>
                  </div>
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
            <a style="cursor: pointer" @click="jumpViewMore('new')">查看更多</a>
          </div>
          <div class="car-tuning" v-for="index in new_game_name.length" :key="index" style="cursor: pointer" @click="jumpGame(newrelease[index - 1])">
            <div class="container">
              <div class="box">
                <img :src="require('../../../ExGame-Asset/Game/' + new_cover[index - 1])" alt=""  style="border-radius:5px;">
                <!--          {{cover[index - 1]}}-->
                <div class="game-detail">
                  <h6>{{ new_game_name[index - 1] }}</h6>
                  <p class="new-epic-button">新品</p>
                  <div>
                    <p class="p-sale-button" v-if="new_discount[index-1]<1">-{{100-new_discount[index-1]*100}}%</p>
                    <p class="sale-price-" v-if="new_discount[index-1]<1">¥{{ new_price[index - 1] }}</p>
                    <p class="sale-price">¥{{numFilter(new_price[index - 1] * new_discount[index - 1])}}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="swiper-slide swiper-third">
          <div class="free-text-button">
            <div class="icon-text">
              <h6>热销榜</h6>
            </div>
            <a style="cursor: pointer" @click="jumpViewMore('hot')">查看更多</a>
          </div>
          <div class="car-tuning" v-for="index in hotsale.length" :key="index" style="cursor: pointer" @click="jumpGame(hotsale[index - 1])">
            <div class="container">
              <div class="box">
                <img :src="require('../../../ExGame-Asset/Game/' + hot_cover[index - 1])" alt="">
                <div class="game-detail">
                  <h6>{{ hot_game_name[index - 1] }}</h6>
                  <div>
                    <p class="sale-price2">¥{{ hot_price[index - 1] }}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="swiper-slide swiper-third">
          <div class="free-text-button">
            <div class="icon-text">
              <h6>好评榜</h6>
            </div>
            <a style="cursor: pointer" @click="jumpViewMore('praise')">查看更多</a>
          </div>
          <div class="car-tuning" v-for="index in praise.length" :key="index" style="cursor: pointer" @click="jumpGame(praise[index - 1])">
            <div class="container">
              <div class="box">
                <img :src="require('../../../ExGame-Asset/Game/' + praise_cover[index - 1])" alt="">
                <div class="game-detail">
                  <h6>{{praise_game_name[index - 1]}}</h6>
                  <div>
                    <p class="sale-price2">¥{{ praise_price[index - 1] }}</p>
                  </div>
                </div>
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

          <div class="swiper-slide swiper-fourth" v-for="index in recommend.length" :key="index" style="cursor: pointer" @click="jumpGame(recommend[index - 1])">
            <div class="container">
              <div class="box">
                <img :src="require('../../../ExGame-Asset/Game/' + recommend_cover[index - 1])" alt=""  style="border-radius:5px;">
                <div class="game-detail">
                  <h6>{{ recommend_game_name[index - 1] }}</h6>
                  <div>
                    <p>¥{{recommend_price[index - 1]}}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="swiper fourth-swipe">
        <h6 class="rec-text">即将上架</h6>
        <div class="swiper-wrapper">
          <div class="swiper-slide swiper-fourth" v-for="index in later.length" :key="index" style="cursor: pointer" @click="jumpGame(later[index - 1])">
            <div class="container">
              <div class="box">
                <img :src="require('../../../ExGame-Asset/Game/' + later_cover[index - 1])" alt=""  style="border-radius:5px;">
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
      </div>
    </div>
    <!--    <buttom-list></buttom-list>-->
    </body>
    <GamePageFooter/>
  </div>
</template>


<script>

import GamePageHeader from "@/views/GamePageHeader";
import Swiper from 'swiper'; // 注意引入的是Swiper
import GamePageFooter from "@/views/GamePageFooter";
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
    GamePageFooter
  },
  data(){
    return{
      ipsearchData:"tp",
      game_id: "",
      search_choice:0,
      // big swiper
      swiper_game_id: [],
      swiper_game_name: [],
      swiper_publish_date:[],
      swiper_price:[],
      swiper_cover:[],
      swiper_ancover:[],
      swiper_general_intro:[],
      // game sale
      asd_right: "0",
      sale_game_id:[],
      sale_game_name: [],
      sale_price:[],
      sale_ancover:[],
      sale_discount:[],
      // game list
      newrelease: [], // 列表1的id
      new_game_name:[],
      new_cover:[],
      new_price:[],
      new_discount:[],
      hotsale: [], // 列表2的id
      hot_game_name:[],
      hot_cover:[],
      hot_price:[],
      hot_discount:[],
      praise: [], // 列表3的id
      praise_game_name:[],
      praise_cover:[],
      praise_price:[],
      praise_discount:[],

      // buttom list
      recommend:[],
      recommend_game_name:[],
      recommend_cover:[],
      recommend_price:[],
      recommend_discount:[],
      later:[],
      later_game_name:[],
      later_cover:[],
      later_price:[],
      later_discount:[]
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
    jumpViewMore(rank_condition){
      console.log('跳转到条件为' + rank_condition +'的ViewMore');
      this.$router.push({name:'ViewMore', query: {rank_condition:rank_condition}});
    },

    jumpGame(game_id) {
      console.log('跳转到' + game_id +'detail');
      // alert('跳转到' + game_id +'detail');
      this.$router.push({name:'GameDetail', query: {game_id:game_id}});
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
                  console.log('get sale discount ' + res.data.discount[i])
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
    async getGameRank(rankname, number=4, ranklist, game_name, price, cover, discount){
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

      this.getGameListInfo(ranklist, game_name, price, discount);
      let i;
      for(i of ranklist){
        cover.push(i + '/Cover/anCover.jpg');
      }
    },
    getGameListInfo(game_id, game_name, price, discount){
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

                for(i in res.data.discount)
                {
                  console.log('get  discount ' + res.data.discount[i])
                  discount.push(res.data.discount[i]) ;
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
    for (let i = 0 ; i <= 5; i++){
      let game_id = '100000019' + i.toString();
      this.swiper_game_id.push(game_id);
      this.swiper_cover.push(game_id + '/Cover/Cover.jpg');
      this.swiper_ancover.push(game_id + '/Cover/anCover.jpg')
    }
    this.getSwiperInfo(this.swiper_game_id);

    // game取数据
    for (let i = 1 ; i <= 6; i++){
      let game_id = '100000019' + i.toString();
      this.sale_game_id.push(game_id);
      this.sale_ancover.push(game_id + '/Cover/anCover.jpg');
    }
    this.getSaleInfo(this.sale_game_id);

    // game list取数据
    this.getGameRank("hot", 5, this.hotsale, this.hot_game_name, this.hot_price, this.hot_cover, this.hot_discount);
    this.getGameRank("new", 5, this.newrelease, this.new_game_name, this.new_price, this.new_cover, this.new_discount);
    this.getGameRank("praise", 5, this.praise, this.praise_game_name, this.praise_price, this.praise_cover, this.praise_discount);

    //buttom list取数据
    this.getGameRank("recommend", 4, this.recommend, this.recommend_game_name, this.recommend_price, this.recommend_cover, this.recommend_discount);
    this.getGameRank("later", 4, this.later, this.later_game_name, this.later_price, this.later_cover, this.later_discount);
  }

}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import "../assets/sea_css/index-style.css";
@import "../assets/sea_css/mixins.css";
@import "../assets/sea_css/cards.css";
@import "../assets/sea_css/variables.css";

#particles-js {
  width: 100%;
  height: calc(320% - 185px);
  position: absolute;
}



</style>
