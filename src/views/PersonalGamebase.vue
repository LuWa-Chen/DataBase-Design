<template>
    <div>
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
        <div class="back">
            <br>
            <hr>
            <br><br><br>

            <div class="container">
                <div class="cards"  v-if="showImg">
                    <div class="cards_item" v-for="(item,index) in this.game_info_set" :title="item" :key="(item,index)" >
                        <div class="cards_itemcontent" >
                            <img   class="pic" :src="require('../../../ExGame-Asset/Game/'+ game_info_set[index].id + '/Cover/anCover.jpg')  " alt="">
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>

<script>
import GamePageHeader from "@/views/GamePageHeader";
export default {
    name:'game-info',
    components: {
        GamePageHeader
    },
    data(){
        return {
            showImg:false,
            game_info_set:[],
        }
    },
    mounted() {
        this.getgameinfo()
    },
    methods:{
        getgameinfo() {
            const self = this;
            self.$axios({
                method:'post',
                url: 'api/user/getUserGameInfo',
                data: {
                    id:this.$store.state.userID,
                }
            }).then( res => {
                if(res.data.result===1) {
                    console.log("数据库连接正常！")
                    self.num=res.data.game_num;
                    console.log(self.num);
                    for(let i=0;i<self.num;i++)
                    {
                        console.log(res.data.game_info_set[i].game_name)
                        self.game_info_set.push(
                            {
                                id:res.data.game_info_set[i].game_id,
                                name:res.data.game_info_set[i].game_name,
                                cover:res.data.game_info_set[i].cover
                            }
                        )
                    }
                    self.showImg = true
                }
                else{
                    alert("数据库链接出错，请重试！");
                }
            }).catch( err => {
                console.log(err);
            })

        }
    }
}

</script>

<style scoped>
#particles-js {
    width: 100%;
    height: calc(100% - 100px);
    position: absolute;
}
.head{
  color:white;
  font-size: 30px;
  text-align:center;
}

img{
  width:300px;
  height: 370px;
}
*{
  box-sizing: border-box;
}
.container {
  display: flex;
  justify-content:center;
  align-items:center;
  height:70vh;
  padding:8px;
}
.cards{
  display:flex;
  width:80%;
  flex-wrap:wrap;
  margin:0 -8px;
}

.cards_item{
  flex-basis:20%;
  padding:0 8px;
  margin:0 0 24px;

}

.cards_itemcontent{
  /*background:darkblue;*/
  width:200px;
  height:280px;
  border-radius:0.25rem;
}

.pic{
  width:200px;
  height:280px;

}

.title{
  color: white;
}



</style>
