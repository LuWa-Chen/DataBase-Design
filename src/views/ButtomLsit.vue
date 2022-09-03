<template>
  <div class="swiper third-swipe">
    
    <div class="swiper fourth-swipe">
      <h6 class="rec-text">为你推荐</h6>
      <div class="swiper-wrapper">
        <div class="swiper-slide swiper-fourth" v-for="index in recommend.length" :key="index">
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
        <div class="swiper-slide swiper-fourth" v-for="index in later.length" :key="index">
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
</template>

<script>
import Swiper from "swiper";

export default {
  name: "ButtomList",
  mounted() {
    new Swiper('.swiper-container', {
      autoplay: true,
      loop: true,
      // 其他配置...
    })
  },
  data() {
    return {
      recommend:[],
      recommend_game_name:[],
      recommend_cover:[],
      recommend_price:[],
      later:[],
      later_game_name:[],
      later_cover:[],
      later_price:[]
    }
  },
  created(){
    //buttom list取数据
    this.getGameRank("recommend", 4, this.recommend, this.recommend_game_name, this.recommend_price, this.recommend_cover);
    this.getGameRank("later", 4, this.later, this.later_game_name, this.later_price, this.later_cover);

  },
  methods:{

    async getGameRank(rankname, number, ranklist, game_name, price, cover){
      const self = this;
      let a = rankname + "排行榜";
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
                alert("排行榜申请数据失败");
                break;
              case 1:
                a = a + "加载成功";
                for(i in res.data.id_list)
                {
                  console.log('buttom ' + res.data.id_list[i])
                  ranklist.push(res.data.id_list[i]) ;
                }
                break;
            }
          })

      if( ranklist == null)
        return;

      this.getGameInfo(ranklist, game_name, price);
      let i;
      for(i of ranklist){
        cover.push(i + '/Cover/anCover.jpg');
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
                alert("ButtomList申请数据失败");
                break;
              case -1:
                alert("ButtomList数据库端出现问题，请联系管理人员");
                break;
              case -2:
                alert("数据库出现问题");
                break;
              case 1:
                a = a + game_id.toString() + "加载成功";

                for(i in res.data.game_name)
                {
                  console.log('buttom_list get   ' + res.data.game_name[i])
                  game_name.push(res.data.game_name[i]) ;
                }

                for(i in res.data.price)
                {
                  console.log('buttom_list get   ' + res.data.price[i])
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
@import "../assets/sea_css/index-style.css";

</style>
