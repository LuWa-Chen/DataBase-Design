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

    <div class="swiper third-swipe" style="left: 11%;">
      <div class="swiper-wrapper">
        <div class="swiper-slide swiper-third">
          <div class="free-text-button">
            <div class="icon-text">
              <h6 style="position:relative;left: 480px;font-size: 25px;">{{ rank_title }}</h6>
              <p style="position:relative;left:-70px;font-size: 19px;top: 38px" v-if="rank_title==='新品榜'">近一个月内发售游戏</p>
            </div>
            <a v-if="rank_title!=='新品榜'" style="cursor: pointer;position:relative; right: -510px" @click="addGameNum()" >查看更多</a>
          </div>
          <div class="car-tuning1" v-for="index in game_cover.length" :key="index" style="cursor: pointer;position:relative;top:30px" @click="jumpGame(game_id[index - 1])">
            <div class="container">
              <div class="box">
                <img :src="require('../../../ExGame-Asset/Game/' + game_cover[index - 1])" alt="">

                <div class="game-detail">
                  <h6>{{ game_name[index - 1] }}</h6>
                  <div>
                    <p class="p-sale-button" style="left: 380px" v-if="game_discount[index-1]<1">-{{100-game_discount[index-1]*100}}%</p>
                    <p class="sale-price-" v-if="game_discount[index-1]<1">¥{{game_price[index - 1]}}</p>
                    <p style="position:relative;left: 63%;top:-75px;word-wrap:break-word;width:350px;line-height: normal;font-size: 15px;overflow: auto;">{{game_intro[index-1]}}</p>
                    <p class="sale-price">¥{{numFilter(game_price[index - 1] * game_discount[index - 1])}}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!--    底栏-->
    <div class="swiper third-swipe">

      <div class="swiper fourth-swipe">
        <h6 class="rec-text">为你推荐</h6>
        <div class="swiper-wrapper">

          <div class="swiper-slide swiper-fourth" v-for="index in recommend.length" :key="index" style="cursor: pointer" @click="jumpGame(recommend[index - 1])">
            <div class="container">
              <div class="box">
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
        </div>
      </div>

      <div class="swiper fourth-swipe">
        <h6 class="rec-text">即将上架</h6>
        <div class="swiper-wrapper">
          <div class="swiper-slide swiper-fourth" v-for="index in later.length" :key="index" style="cursor: pointer" @click="jumpGame(later[index - 1])">
            <div class="container">
              <div class="box">
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
      </div>
    </div>
    <GamePageFooter/>
    </body>
  </div>
</template>

<script>
import Swiper from "swiper";
import GamePageHeader from "@/views/GamePageHeader";
import GamePageFooter from "@/views/GamePageFooter";

export default {
  name: "SearchPage",
  components: {
    GamePageHeader,
    GamePageFooter
  },
  mounted() {
    new Swiper('.swiper-container', {
      observer:true,//修改swiper自己或子元素时，自动初始化swiper
      observeParents:true,//修改swiper的父元素时，自动初始化swiper
      autoplay: true,
      loop: false,
      spaceBetween: 30,
      // 其他配置...
    })
  },
  data() {
    return {
      rank_condition:'',
      rank_title:'',
      game_id:[],
      game_name:[],
      game_cover:[],
      game_price:[],
      game_intro:[],
      game_discount:[],
      game_num:4,

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
  watch:{
  },
  created(){
    switch (this.$route.query.rank_condition){
      case 'hot':
        this.rank_title='热销榜';
        break;
      case 'new':
        this.rank_title='新品榜';
        break;
      case 'praise':
        this.rank_title='好评榜';
        break;
    }

    this.game_num =8;
    this.rank_condition = this.$route.query.rank_condition;
    this.getGameRank(this.rank_condition, this.game_num, this.game_id, this.game_name, this.game_price, this.game_cover, this.game_intro, this.game_discount);
    //buttom list取数据
    this.getGameRank("recommend", 5, this.recommend, this.recommend_game_name, this.recommend_price, this.recommend_cover, this.recommend_discount);
    this.getGameRank("later", 5, this.later, this.later_game_name, this.later_price, this.later_cover, this.later_discount);
  },
  methods:{
    numFilter(value) {
      return parseFloat(value).toFixed(2);
    },
    addGameNum(){
      console.log('增加2个游戏');
      this.game_num += 2;
      this.game_id=[];
      this.game_name=[];
      this.game_price=[];
      this.game_cover=[];
      this.game_intro=[];
      this.game_discount=[];
      this.getGameRank(this.rank_condition, this.game_num, this.game_id, this.game_name, this.game_price, this.game_cover, this.game_intro, this.game_discount);
    },
    jumpGame(game_id) {
      console.log('跳转到' + game_id +'detail');
      // alert('跳转到' + game_id +'detail');
      this.$router.push({name:'GameDetail', query: {game_id:game_id}});
    },
    //game list
    async getGameRank(rankname, number=4, ranklist, game_name, price, cover, game_intro, discount){
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
                // ranklist = [];
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

      await this.getGameListInfo(ranklist, game_name, price, game_intro, discount);
      let i;
      // cover = [];
      for(i of ranklist){
        cover.push(i + '/Cover/anCover.jpg');
      }
    },
    getGameListInfo(game_id, game_name, price, game_intro, discount){
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
                  console.log('get   ' + res.data.game_name[i]);
                  game_name.push(res.data.game_name[i]) ;
                }

                for(i in res.data.price)
                {
                  console.log('get   ' + res.data.price[i]);
                  price.push(res.data.price[i]) ;
                }

                for (i in res.data.general_intro)
                {
                  console.log('get game_intro  ' + res.data.general_intro[i]);
                  if (res.data.general_intro[i].length < 60)
                    game_intro.push(res.data.general_intro[i]);
                  else {
                    game_intro.push(res.data.general_intro[i].slice(0,60) + '……');
                  }
                }

                for (i in res.data.discount)
                {
                  console.log('get   ' + res.data.discount[i]);
                  discount.push(res.data.discount[i]);
                }

                break;

            }
          })
          .catch( err=>{
            console.log(err);
          })
    }
  }
}
</script>

<style scoped>
#particles-js {
  width: 100%;
  height: calc(180% - 100px);
  position: absolute;
}
</style>
