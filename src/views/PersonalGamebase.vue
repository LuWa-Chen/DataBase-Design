y<template>
    <div class="back">
        <div style="overflow: hidden">
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
            <div class="addBorder" style="margin: auto;">
                <p class="lib-title clearbox">我的游戏库</p>
                <div class="container clearbox">
                    <div class="games-board addBorder">
                        <div class="cards-left fl"  v-if="showImg">
                            <div>快速启动</div>
                            <ul>
                                <li class="cards_item-l" v-for="(item,index) in this.downLoadGame" :title="item" :key="index">
                                    <div class="m-cards-left" @mouseleave="onLeave(index,'win-l-')" >
                                        <img  class="fl" :src="require('../../../ExGame-Asset/Game/'+ item.id + '/Cover/anCover.jpg')  " alt="">
                                        <span class="fl" style="display: inline-block;width: 140px">
                                    <div class="fl" style="width: 120px;overflow: hidden;height: 40px;">
                                        {{item.name}}
                                    </div>
                                    <div  @mouseenter="onHover(index,'win-l-')" class="fr" style="height: 40px;line-height: 40px;">
                                         <svg   xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-three-dots-vertical" viewBox="0 0 16 16">
                                         <path d="M9.5 13a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
                                        </svg>
                                    </div>
                                </span>
                                        <div @mouseleave="onLeave(index,'win-l-')"  :id="getWinID(index,'win-l-')" class="c-win clearbox" style="width: 105px;margin-left: 95px;margin-top: 45px">
                                            <div style="height: 5px"></div>
                                            <ul>
                                                <li>
                                                    <div  @click="click2Detail(item.id)"> &nbsp;{{leftItem[0]}}</div>
                                                </li>
                                                <li>
                                                    <div @click="click2DeleteGame(index)"> &nbsp;{{leftItem[1]}}</div>
                                                </li>
                                            </ul>
                                            <div style="height: 5px"></div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div  class="cards-right fr"  v-if="showImg">
                            <div>我的游戏</div>
                            <ul>
                                <li class="cards_item-r" v-for="(item,index) in this.game_info_set" :title="item" :key="index">
 		
                                   <div class="m-cards-right fl" @mouseleave="onLeave(index,'win-r-')"  >
                                <router-link :to="{name:'GameBaseDetail', query: {game_id: item.id,is_download:downLoadList[index]} }">
                                        <img :src="require('../../../ExGame-Asset/Game/'+ item.id + '/Cover/anCover.jpg')  " alt="">
 		</router-link>
                                        <div class="m-game-name" style="color: white">
                                            <div class="fl" style="width: 150px;overflow: hidden;height: 25px;line-height: 20px">
                                                {{item.name}}
                                            </div>
                                            <svg  @mouseenter="onHover(index,'win-r-')"  xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-three-dots-vertical" viewBox="0 0 16 16">
                                                <path d="M9.5 13a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
                                            </svg>
                                        </div>
                                        <div class="clearbox" style="color: white">
                                            <div class="download-button clearbox" @click="click2DownLoad(index)" v-if="downLoadList[index]===false">
                                                <svg  style="position: relative;top:3px" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-download" viewBox="0 0 16 16">
                                                    <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z"/>
                                                    <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3z"/>
                                                </svg>
                                                下载
                                            </div>
                                            <div class="download-button clearbox" v-else>
                                                已下载
                                            </div>
                                        </div>
                                        <div @mouseleave="onLeave(index,'win-r-')"  :id="getWinID(index,'win-r-')" class="c-win clearbox" style="width: 105px;margin-left: 85px;top: -25px;color:white">
                                            <div style="height: 5px"></div>
                                            <ul>
                                                <li>
                                                    <div @click="click2Detail(item.id)"> &nbsp;{{rightItem[0]}}</div>
                                                </li>
                                                <li>
                                                    <div @click="click2RemoveGame(index)"> &nbsp;{{rightItem[1]}}</div>
                                                </li>
                                            </ul>
                                            <div style="height: 5px"></div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div style="height: 100px"></div>
            </div>
	<GamePageFooter/>
        </div>
    </div>
</template>

<script>

import GamePageHeader from "@/views/GamePageHeader";
import GamePageFooter from "@/views/GamePageFooter";
export default {
    name:'game-info',
    components: {
        GamePageHeader,
GamePageFooter
    },
    data(){
        return {
            showImg:false,
            leftItem:['前往游戏主页','卸载游戏'],
            rightItem:['前往游戏主页','移除游戏'],
            game_info_set:[],
            downLoadList:[],
            downLoadGame:[],
        }
    },
    mounted() {
        this.getgameinfo()
        var t = this
        setTimeout(function (){
            t.click2DownLoad(0)
        },800)
    },
    methods:{
        getWinID(index ,pre){
            return pre + index.toString()
        },
        click2DeleteGame(index){
            console.log(index)
            var temp=[]
            for(let i=0;i<this.downLoadGame.length;i++)
            {
                if(i!==index)
                    temp.push(this.downLoadGame[i])
            }
            this.downLoadGame = temp
            this.downLoadList[index] = false
        },
        click2Detail(id){
             this.$router.push({name:'GameDetail',query: {game_id:id}});
        },
        click2RemoveGame(index){
            var temp =[]
            for(let i=0;i<this.game_info_set.length;i++)
            {
                if(i!==index)
                    temp.push(this.game_info_set[i])
            }
            this.sendDeleteRequest(this.game_info_set[index].id)
            this.game_info_set = temp
        },
        onHover(index,pre){
            document.getElementById(this.getWinID(index,pre)).style.display = 'block'
        },
        onLeave(index,pre){
            document.getElementById(this.getWinID(index,pre)).style.display = 'none'
        },
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
                                cover:res.data.game_info_set[i].cover,
                            }
                        )
                        self.downLoadList.push(false)
                    }
                    self.showImg = true
                }
                else{
                    alert("数据库链接出错，请重试！");
                }
            }).catch( err => {
                console.log(err);
            })

        },
        sendDeleteRequest(gid){
            const self = this;
            self.$axios({
                method:'post',
                url: 'api/user/deleteUserGame',
                data: {
	    
                    user_id:this.$store.state.userID,
                    game_id:gid
                }
            }).then( res => {
	console.log(res.data.result);
                if(res.data.result===1) {
                    console.log("删除成功！")
                }
                else{
                    alert("数据库链接出错，请重试！");
                }
            }).catch( err => {
                console.log(err);
            })
        },
        click2DownLoad(index){
            if(this.downLoadList[index]===false)
            {
                this.downLoadGame.push(this.game_info_set[index])
                this.downLoadList[index] = true
            }
        }
    }
}


</script>

<style scoped>
#particles-js {
    width: 100%;
    height: calc(100% - 100px);
    position: absolute;
    z-index: 0;
}
.back{
    /*margin-top:80px;*/
    background:#2a3cad;
    background-image:url(../assets/img/back.png) ;
    background-size:100%;
    overflow: hidden;
}
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
.addBorder{
    /*border: 1px solid red;*/
    position:relative;
}
::-webkit-scrollbar
{
    width: 6px;
    background-color: #F5F5F5;
}

::-webkit-scrollbar-thumb
{
    background-color: #999;
}

svg:hover{
    border-radius: 5px;
    height: 20px;
    width: 18px;
    background-color:rgb(211 ,211, 211, 0.8);
}
.clearbox{
    clear:both;
}

.lib-title{
    font-size: 20px;
    font-weight: bolder;
    height: 80px;
    line-height: 80px;
    margin: auto;
    width: 1400px;
}

.c-win{
    background-color: black;
    border-radius: 5px;
    z-index: 999;
    position: relative;
    display: none;
}
.c-win li{
    height: 30px;
    font-size: 14px;
    line-height: 30px;
    margin-left: 5px;
    margin-right: 5px;
    border-radius: 5px;
}
.c-win li:hover{
    background-color:rgb(211 ,211, 211, 0.4);
}
.container {
    width: 100%;
    overflow: hidden;
    margin: auto;
}
.games-board{
    width: 1400px;
    margin: auto;
    min-height: 500px;
}
.cards-left{
    width: 250px;
    height: 580px;
    overflow-y: auto;
    overflow-x: hidden;
    /*border: 2px red solid;*/
}
.cards-right{
    width:1100px;
    height: 650px;
    overflow-y: auto;
    overflow-x: hidden;
    /*border: 2px red solid;*/
    margin-left: 10px;
}
.cards_item-l{
    margin-top: 10px;
    margin-left: 5px;
}
.cards_item-r{
    margin-top: 20px;
    margin-left: 15px;
}

.m-cards-left{
    width:200px;
    height: 70px;
    /*border: 1px red solid;*/
    border-radius: 10px;
}
.m-cards-left:hover{
    background-color:rgb(211 ,211, 211, 0.4);
}
.m-cards-left img{
    width: 40px;
    margin-top: 8px;
    border-radius: 10px;
    margin-left: 8px;
}
.m-cards-left span{
    height: 40px;
    margin-top: 15px;
    margin-left: 10px;
    line-height: 40px;
    font-size: 16px;
    display: inline-block;
    /*border: red 1px solid;*/
}
.m-cards-right{
    width:200px;
    height: 290px;
    /*border: 1px red solid;*/
    margin-bottom: 20px;
    margin-right: 10px;
    border-radius: 10px;
}
.m-cards-right:hover{
    background-color:rgb(211 ,211, 211, 0.4);
}

.m-cards-right img{
    margin-left: 20px;
    margin-top: 15px;
    width: 160px;
    border-radius: 10px;
}
.m-game-name{
    margin-left: 20px;
    margin-top: 10px;
}
.download-button{
    margin-left: 20px;
    display: inline-block;
    height: 25px;
    line-height: 25px;
    font-size: 12px;
}
</style>
