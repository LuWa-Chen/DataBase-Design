<!--1952168 张宇-->
<template>
    <div style="width: 1055px;height:300px;color:black;margin: auto;border-radius: 10px;background-color: #e0e0e0" class="clearbox">
    <div class="m-recommended clearbox">
            <div class="m-tt fl">推荐</div>
            <ul style="margin: auto;width: 1000px;" class="clearbox" v-if=" gameList.length===gameNum">
                <li v-for="(game,index) in gameList" :key="index">
                    <div class="m-game-rec fl">
                        <router-link :to="{name:'GameDetail',query:{game_id:gameList[index]}}">
                            <div style="height: 125px;overflow: hidden">
                                <img :src="require('../../../ExGame-Asset/' +  gameIntro[index].coverPath)">
                            </div>
                            <p class="game-name"> {{ gameInfo[index].gameName}}</p>
                            <p class="game-price">￥{{ (gameInfo[index].price * gameInfo[index].discount).toFixed(2)}}</p>
                        </router-link>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
export default {
    name: "GameRecommended",
    props:['game_id'],
    data(){
        return{
            gameList:[],
            gameInfo:[],
            gameIntro:[],
            gameNum:0,
        }
    },
    mounted() {
        this.getData(this.game_id);
        setTimeout(this.fun,800)
    },
    methods:{
        getData:function(gid){
            if(gid.length===0)
            {
                alert('gid 不能为空')
                return;
            }

            this.$axios.post('api/gamedetail/getRecommendedGames', {
                game_id: gid,
                user_id: '0000000012'
            }).then( res => {
                switch(res.data.result){
                    case 1:
                        console.log("推荐游戏 请求成功");
                        break;
                    default:
                        console.log('推荐游戏 请求失败')
                        break;
                }
                for(let i in res.data.game_list)
                {
                    console.log(res.data.game_list[i])
                    this.gameList.push(
                        res.data.game_list[i]
                    )
                }
            }).catch( err => {
                console.log(err);
            })

        },
        getGameInfo:function (gid){
            var self = this;
            console.log('++++++')
            if(gid.length !== 0)
            {
                this.$axios.post('api/gamedetail/getGameCarousel', {
                    game_id: gid
                }).then( res => {
                    switch(res.data.result){
                        case 1:
                            console.log("GameCarousel -r 请求成功");
                            break;
                        default:
                            console.log('GameCarousel -r 请求失败')
                            break;
                    }
                    self.gameInfo.push(
                        {
                            gameName:res.data.game_name,
                            price:res.data.price,
                            discount:res.data.discount,
                        }
                    )
                    console.log(res.data.discount)
                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏明不能为空')
            }
        },
        getAboutInfo:function (gid){
            var self = this;
            console.log('++++++ getAboutData')
            if(gid.length !== 0)
            {
                this.$axios.post('api/gamedetail/getGameIntro', {
                    game_id: gid
                }).then( res => {
                    let i;
                    switch(res.data.result){
                        case 1:
                            console.log("GameCarousel 请求成功");
                            break;
                        default:
                            console.log('GameCarousel 请求失败')
                            break;
                    }
                    for(i in res.data.about_game)
                    {
                        self.gameIntro.push({
                            coverPath:res.data.about_game[i].poster,
                            intro:res.data.about_game[i].content
                        }) ;
                        this.gameNum++
                    }
                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏ID不能为空')
            }
        },
        fun(){
            for(let i = 0;i<this.gameList.length;i++)
            {
                this.getGameInfo(this.gameList[i]);
                this.getAboutInfo(this.gameList[i]);
            }
        }
    }
}
</script>
<style scoped>
*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}
ul, ol{
    list-style: none;
}
a{
    text-decoration: none;
    color: #333;
}
.fl{
    float: left;
}
.fr{
    float: right;
}
.clearbox{
    clear:both;
}

.m-tt{
    font-weight: normal;
    margin-bottom: 10px;
    margin-top: 20px;
    font-size: 20px;
    /*color: #eeeeee;*/
}

.m-recommended{
    width: 1005px;
    margin: 0 auto;
    overflow:hidden;
}
.m-game-rec{
    height: 220px;
    width: 230px;
    margin: 10px 10px;
    border-radius: 10px;
    overflow: hidden;
    background-color: white;
}
.m-game-rec img{
    /*border-radius: 10px 10px 0 0;*/
    height: 125px;
    width: 100%;
    overflow: hidden;
    transition: all 0.6s;
}
.m-game-rec:hover img {
    transform: scale(1.2, 1.2);
}

.game-name{
    margin-left: 20px;
    margin-top: 15px;
    margin-right: 10px;
    font-weight: bolder;
    font-size: 20px;
    text-align: left;
}
.game-price{
    margin-left: 20px;
    font-size: 14px;
    margin-top: 10px;
    color:#666666;
    text-align: left;
}
</style>
