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
                <p class="lib-title clearbox">用户游戏库</p>
                <div class="container clearbox">
                    <div class="games-board addBorder">
                        <div class="cards-left fl"  v-if="showImg">
<!--                            <div>添加游戏</div>-->
                          <a href="#" style="cursor: pointer;color: #F0F0F0;top: 2px" @click="addUserGame()">添加游戏</a>
                          <input id="user-game-id" placeholder="游戏id" type="text" autocomplete="off" style="width: 150px;left: 20px;position: relative;top: 2px">

                        </div>
                        <div  class="cards-right fr"  v-if="showImg">
                            <div>用户游戏</div>
                            <ul>
                                <li class="cards_item-r" v-for="(item,index) in this.game_info_set" :title="item" :key="index">
                                    <div class="m-cards-right fl" @mouseleave="onLeave(index,'win-r-')"  >
                                        <img :src="require('../../../ExGame-Asset/Game/'+ item.id + '/Cover/anCover.jpg')  " alt="">
                                        <div class="m-game-name">
                                            <div class="fl" style="width: 150px;overflow: hidden;height: 25px;line-height: 20px">
                                                {{item.name}}

                                            </div>
                                            <svg  @mouseenter="onHover(index,'win-r-')"  xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-three-dots-vertical" viewBox="0 0 16 16">
                                                <path d="M9.5 13a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0zm0-5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/>
                                            </svg>
                                        </div>
                                        <div class="clearbox">
                                            <div class="download-button clearbox" @click="click2DownLoad(index)">
<!--                                                <svg  style="position: relative;top:3px" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-download" viewBox="0 0 16 16">-->
<!--                                                    <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z"/>-->
<!--                                                    <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3z"/>-->
<!--                                                </svg>-->
                                              {{item.id}}
                                            </div>
                                        </div>
                                        <div @mouseleave="onLeave(index,'win-r-')"  :id="getWinID(index,'win-r-')" class="c-win clearbox" style="width: 105px;margin-left: 85px;top: -25px">
                                            <div style="height: 5px"></div>
                                            <ul>
<!--                                                <li>-->
<!--                                                    <div @click="click2Detail(item.id)"> &nbsp;{{rightItem[0]}}</div>-->
<!--                                                </li>-->
                                                <li>
                                                    <div @click="deleteUserGame(item.id)"> {{rightItem[1]}}</div>
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
      // 为一个用户增加一个游戏
      async addUserGame(user_id) {
        user_id = this.$route.query.userID;
        let game_id = document.getElementById('user-game-id').value;
        if (game_id === '')
        {
          alert("请输入内容");
          return
        }

        console.log("为用户" + user_id + "增加游戏" + game_id);
        let self = this;
        await self.$axios({
          method:'post',
          url: 'api/user/addUserGame',
          data: {
            user_id:user_id,
            game_id:game_id
          }
        })
            .then(res=>{
              switch (res.data.result){
                case 0:
                  alert("为用户" + user_id + "增加游戏" + game_id + "失败");
                  break;
                case -1:
                  alert("为用户" + user_id + "增加游戏" + game_id +"数据库连接失败");
                  break;
                case 1:
                  alert("为用户" + user_id + "增加游戏" + game_id + "成功");

                  this.game_info_set = [];
                  this.getgameinfo();

                  break;
              }
            })
      },

      // 删除一个用户库里的一个游戏
      async deleteUserGame(game_id){
        let user_id = this.$route.query.userID;
        console.log("为用户" + user_id + "删除游戏" + game_id);
        let self = this;
        await self.$axios({
          method:'post',
          url: 'api/user/deleteUserGame',
          data: {
            user_id:user_id,
            game_id:game_id
          }
        })
            .then(res=>{
              switch (res.data.result){
                case 0:
                  alert("为用户" + user_id + "删除游戏" + game_id + "失败");
                  break;
                case -1:
                  alert("为用户" + user_id + "删除游戏" + game_id +"数据库连接失败");
                  break;
                case 1:
                  alert("为用户" + user_id + "删除游戏" + game_id + "成功");
                  this.game_info_set = [];
                  this.getgameinfo();

                  break;
              }
            })
      },

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
            console.log(id)
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
                    id:this.$route.query.userID,
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
    height: 100%;
}
*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

body{
    font-family: 'Microsoft YaHei', sans-serif;
    color: #333;
    font-size: 12px;
    min-height: 1000px;
    min-width: 1400px;
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
