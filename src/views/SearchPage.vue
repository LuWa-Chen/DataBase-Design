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

  <!--左上角搜索栏-->
  <div class="search-bar">
    <i class="fas fa-search search-icon-mobile"></i>
    <div class="search-bar-main">
      <div class="search-div">
        <i class="fas fa-search search-icon-pc" @click="search()"></i>
        <form>
          <input id="searchtext" placeholder="搜索" type="text" autocomplete="off">
        </form>

      </div>
      <ul>
        <li><a href="#">发现</a></li>
        <li><a href="#">个人</a></li>
        <li><a href="#">库</a></li>
      </ul>
    </div>
  </div>

  <div class="swiper third-swipe" >
    <div class="swiper-wrapper">
      <div class="swiper-slide swiper-third" >
        <div class="free-text-button">
          <div class="icon-text">
            <h6>筛选</h6>
            <ul>
              <li id="is_dlc_li" style="background-color: #2a2a2a; "><a id="is_dlc_a" href="#" style="color: #F0F0F0;cursor: pointer;" @click="switchOption(is_dlc)">DLC</a></li>
            </ul>
            <ul>
              <li id="is_on_sale"><a href="#" @click="switchOption(is_on_sale)">折扣</a></li>
            </ul>
          </div>

        </div>

        <div class="free-text-button">
          <div class="icon-text">
            <h6>排序方式</h6>
            <ul>
              <li id="price_rank"><a href="#" @click="switchCondition(1)">价格</a></li>
            </ul>
            <ul>
              <li id="sale_rank"><a href="#" @click="switchCondition(2)">销量</a></li>
            </ul>
            <ul>
              <li id="time_rank"><a href="#" @click="switchCondition(3)">发布时间</a></li>
            </ul>

          </div>

        </div>

      </div>

      <div class="swiper-slide swiper-third" >
        <div class="free-text-button">

        </div>
        <div class="car-tuning" v-for="index in search_list.length" :key="index">
          <div class="container">
            <div class="box">
          <img :src="require('../../../ExGame-Asset/Game/' + search_cover[index - 1])" alt="">
          <div class="game-detail">
            <h6>{{ search_game_name[index - 1] }}</h6>
            <p class="new-epic-button">新品</p>
            <div>
              <p class="p-sale-button">-20%</p>
              <p class="sale-price-">{{ search_price[index - 1] }}</p>
              <p class="sale-price">{{ search_price[index - 1] }}</p>
            </div>
          </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

<!--buttom list-->
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

  </body>


  </div>
</template>

<script>
import Swiper from "swiper";
import GamePageHeader from "@/views/GamePageHeader";

export default {
  name: "SearchPage",
  inject:['reload'],
  components: {
    GamePageHeader},
  mounted() {
    new Swiper('.swiper-container', {
      autoplay: true,
      loop: true,
      // 其他配置...
    })
  },
  data() {
    return {
      search_name:'',
      search_list:[],
      search_game_name:[],
      search_cover:[],
      search_price:[],
      is_dlc: 2,
      is_on_sale: 2,
      game_or_publisher: 0,
      rank_condition: 0,
      search_page:1,
      all_page:-1,

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
  watch:{
  },
  created(){
    if(this.$route.query.search_choice==='按游戏名称搜索'){
      this.game_or_publisher = 0;
    }
    else{
      this.game_or_publisher = 1;
    }

    this.search_name = this.$route.query.search_name
    this.searchGame(this.search_name);

    //buttom list取数据
    this.getGameRank("recommend", 4, this.recommend, this.recommend_game_name, this.recommend_price, this.recommend_cover);
    this.getGameRank("later", 4, this.later, this.later_game_name, this.later_price, this.later_cover);

  },
  methods:{
    jumpGame(game_id) {
      console.log('跳转到' + game_id +'detail');
      // alert('跳转到' + game_id +'detail');
      this.$router.push({name:'GameDetail', query: {game_id:game_id}});
    },
    switchOption(option){
      if (option==1){
        option=0;
      }
      else{
        option=1;
      }
      this.search();
    },
    switchCondition(condition){
      this.rank_condition = condition;
      this.search();
    },

    search(){
      let a = document.getElementById("searchtext").value;
      if (a != null)
        this.search_name = a;
      else {
        alert("请输入搜索内容")
        return;
      }
      this.searchGame(a);
    },

    async searchGame(name){
      console.log("search  " + this.game_or_publisher);
      console.log("search  " + name);
      console.log("search  " + this.is_on_sale);
      console.log("search  " + this.is_dlc);
      console.log("search  " + this.rank_condition);
      console.log("search  " + this.search_page);

      const self = this;
      await self.$axios({
        method:'post',
        url: 'api/library/GetQueryNameList',
        data: {
          name:name,
          game_or_publisher:this.game_or_publisher,
          is_on_sale:this.is_on_sale,
          is_DLC:this.is_dlc,
          rank_condition:this.rank_condition,
          page:this.search_page
        }
      })
          .then(res=>{

            console.log(res.data.result)
            let i;
            switch (res.data.result){
              case 0:
                alert("查询不到该游戏");
                break;
              case -1:
                alert("searchGame数据库连接失败");
                break;
              case 1:
                this.search_list = [];
                this.search_game_name = [];
                this.search_cover = [];
                this.search_price = [];

                for(i in res.data.id_list)
                {
                  console.log('search get   ' + res.data.id_list[i]);
                  this.search_list.push(res.data.id_list[i]);
                }
                this.all_page = res.data.all_page;
                break;
            }
          })
      if (this.search_list == null)
        return;

      this.getGameInfo(this.search_list, this.search_game_name, this.search_price)
      let i;
      for(i of this.search_list){
        this.search_cover.push(i + '/Cover/anCover.jpg');
      }

    },
    getGameInfo(game_id, game_name, price){
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
                alert("search申请数据失败");
                break;
              case -1:
                alert("search数据库端出现问题，请联系管理人员");
                break;
              case -2:
                alert("数据库出现问题");
                break;
              case 1:
                for(i in res.data.game_name)
                {
                  console.log('search get  ' + res.data.game_name[i])
                  game_name.push(res.data.game_name[i]) ;
                }

                for(i in res.data.price)
                {
                  console.log('search get  ' + res.data.price[i])
                  price.push(res.data.price[i]) ;
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
        cover.push(i + '/Cover/anCover.jpg');
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
  }
}
</script>

<style scoped>

</style>
