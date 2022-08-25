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

  <div class="swiper third-swipe" >
    <div class="swiper-wrapper">
      <div class="swiper-slide swiper-third" >
        <div class="free-text-button">
          <div class="icon-text">
            <h6>筛选</h6>
            <ul>
              <li id="is_dlc"><a href="#">DLC</a></li>
            </ul>
            <ul>
              <li id="is_on_sale"><a href="#">折扣</a></li>
            </ul>
          </div>

        </div>

        <div class="free-text-button">
          <div class="icon-text">
            <h6>排序方式</h6>
            <ul>
              <li id="price_rank"><a href="#">价格</a></li>
            </ul>
            <ul>
              <li id="sale_rank"><a href="#">销量</a></li>
            </ul>
            <ul>
              <li id="time_rank"><a href="#">发布时间</a></li>
            </ul>

          </div>

        </div>

      </div>

      <div class="swiper-slide swiper-third" >
        <div class="free-text-button">

        </div>
        <div class="car-tuning" v-for="index in search_list.length" :key="index">
          <img :src="require('../../../ExGame-Asset/Game/' + later_cover[index - 1])" alt="">
          <div class="game-detail">
            <h6>{{ later_game_name[index - 1] }}</h6>
            <p class="new-epic-button">新品</p>
            <div>
              <p class="p-sale-button">-20%</p>
              <p class="sale-price">{{ later_price[index - 1] }}</p>
              <p>{{ later_price[index - 1] }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
<!--  <buttom-list></buttom-list>-->
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
      all_page:-1
    }
  },
  created(){
    if(this.$route.query.search_choice==='按游戏名称搜索'){
      this.game_or_publisher = 0;
    }
    else{
      this.game_or_publisher = 1;
    }

    this.search_name = "God";
    this.searchGame(this.search_name);

  },
  methods:{
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
          game_or_publisher:0,
          is_on_sale:2,
          is_DLC:2,
          rank_condition:0,
          page:1
        }
      })
          .then(res=>{
            alert("res.data.result")
            console.log(res.data.result)
            let i;
            switch (res.data.result){
              case 0:
                alert("searchGame申请数据失败");
                break;
              case -1:
                alert("数据库连接失败");
                break;
              case 1:
                alert("name")
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
        this.search_cover.push(i + '/Cover/ancover.jpg');
      }

    },
    getGameInfo(game_id, game_name, price){
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
                a = a + game_id.toString() + "加载成功";

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
