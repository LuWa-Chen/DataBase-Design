<template>
    <div class="canvas">
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
           <div style="height:100px"></div>
        <div class="base-detail">
            <div class="base-detail-left fl">
                <div style="margin: auto;width: 400px;overflow: hidden">
                    <div>
                        <img :src="require('../../../ExGame-Asset/Game/' + this.game_id +'/Cover/Cover.jpg')">
                    </div>
                    <div style="
                    text-align: center;
                    height: 60px;
                    line-height: 50px;
                    font-weight: bolder;
                    font-size: 30px">
                       - {{gameName}} -
                    </div>
                    <div class="go-detail">
                        <router-link style="font-size:16px;color: white;text-decoration: none" :to="{name:'GameDetail',query:{game_id:this.game_id}}">
                            <span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-cart-check" viewBox="0 0 16 16">
                                <path d="M11.354 6.354a.5.5 0 0 0-.708-.708L8 8.293 6.854 7.146a.5.5 0 1 0-.708.708l1.5 1.5a.5.5 0 0 0 .708 0l3-3z"/>
                                <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1H.5zm3.915 10L3.102 4h10.796l-1.313 7h-8.17zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"/>
                            </svg>
                            前往商城
                    </span>
                        </router-link>
                    </div>

                    <div class="go-column">
                        <router-link style="font-size:16px;color: white;text-decoration: none"  :to="{name:'GameColumn',query:{game_id:this.game_id}}">
                            <span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-chat-left-text" viewBox="0 0 16 16">
                                    <path d="M14 1a1 1 0 0 1 1 1v8a1 1 0 0 1-1 1H4.414A2 2 0 0 0 3 11.586l-2 2V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12.793a.5.5 0 0 0 .854.353l2.853-2.853A1 1 0 0 1 4.414 12H14a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"/>
                                    <path d="M3 3.5a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zM3 6a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9A.5.5 0 0 1 3 6zm0 2.5a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5a.5.5 0 0 1-.5-.5z"/>
                                </svg>
                                前往论坛
                            </span>
                        </router-link>
                    </div>
                    <div class="create-comment">
                        <div class="go-start clearbox">
                            <span v-if="isDownLoad===false">安装</span>
                            <span v-else>启动</span>
                        </div>
                        <div style="height: 50px"></div>
                        <div style="font-size: 28px;height: 40px"><h2>评测</h2></div>
                        <div class="create-rate">
                            <p style="margin-left:5px;">请为该游戏打分（范围为1-5，1分是特别不推荐，5分是特别推荐）</p>
                            <span id="star1"
                                  @mouseenter="picStar(1)"
                                  @mouseleave="depicStar(1)"
                                  @click="click2ChooseStar(1)"
                                  style="color: #e7e1cd;font-size: 50px">★</span>
                            <span id="star2"
                                  @mouseenter="picStar(2)"
                                  @mouseleave="depicStar(2)"
                                  @click="click2ChooseStar(2)"
                                  style="color: #e7e1cd;font-size: 50px">★</span>
                            <span id="star3"
                                  @mouseenter="picStar(3)"
                                  @mouseleave="depicStar(3)"
                                  @click="click2ChooseStar(3)"
                                  style="color: #e7e1cd;font-size: 50px">★</span>
                            <span id="star4"
                                  @mouseenter="picStar(4)"
                                  @mouseleave="depicStar(4)"
                                  @click="click2ChooseStar(4)"
                                  style="color: #e7e1cd;font-size: 50px">★</span>
                            <span id="star5"
                                  @mouseenter="picStar(5)"
                                  @mouseleave="depicStar(5)"
                                  @click="click2ChooseStar(5)"
                                  style="color: #e7e1cd;font-size: 50px">★</span>
                        </div>
                        <p style="font-size: 12px;color: #666666;margin-top: 20px">请描述您为什么喜欢或不喜欢该产品，以及您是否愿意向他人推荐此游戏。请注意礼貌用词，并遵循规则及指引。</p>
                        <div class="create-description clearbox">
                            <textarea class="fl" v-model="comment"  placeholder="发一个友善的评论"  ref="comment" id="textarea" @keydown="getHight"></textarea>
                            <span @click="click2Send(star,comment)" class="fr" style=
                                "display: inline-block;width: 80px;height: 60px;
                              text-align: center;
                              margin-top: 10px;
                              margin-left: 5px;
                              border: #666666 1px solid;
                              background-color: #3B9CFF;
                              line-height: 60px;
                              font-size: 16px;
                              border-radius: 5px;
                              color: white;">
                            发布
                            <svg style="position:relative;top: 3px" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-send" viewBox="0 0 16 16">
                                <path d="M15.854.146a.5.5 0 0 1 .11.54l-5.819 14.547a.75.75 0 0 1-1.329.124l-3.178-4.995L.643 7.184a.75.75 0 0 1 .124-1.33L15.314.037a.5.5 0 0 1 .54.11ZM6.636 10.07l2.761 4.338L14.13 2.576 6.636 10.07Zm6.787-8.201L1.591 6.602l4.339 2.76 7.494-7.493Z"/>
                            </svg>
                        </span>
                        </div>
                    </div>
                    <div class="clearbox" style="height: 10px"></div>

                    <div style="height: 10px"></div>
                </div>
            </div>
            <div class="base-detail-right fl">
                <div class="lib-intro">
                    <div class="fl" style="overflow:hidden"ref="intro">
                        <img  ref="intro-img" :src="require('../../../ExGame-Asset/Game/' + this.game_id +'/Cover/anCover.jpg')">
                    </div>
                    <div class="lib-game-intro fl">
                        {{intro}}
                    </div>
                </div>
                <div v-if="newsList.length!==0">
                    <div class="news-board clearbox">
                        <div class="board-img fl">
                            <img :src="require('../../../ExGame-Asset/' + newsList[showPos].post_cover)">
                        </div>
                        <div class="board-info fl">
                            <div class="fl" style="margin-left: 20px;margin-top: 20px;color: white">新闻</div>
                            <div class="fr" style="margin-left: 20px;margin-top: 20px;color: white">{{newsList[showPos].post_time}}</div>
                            <div class="clearbox"><div style="height: 10px"></div>
                                <div class="news-title">{{newsList[showPos].post_title}}</div>
                                <div class="news-content">{{newsList[showPos].content_text}}</div>
                            </div>
                        </div>
                    </div>
                    <div class="clearbox" style="height: 0"></div>
                    <div class="news-list clearbox">
                        <span class="iconfont icon-arrow-left fl"  style="margin-left: 10px;margin-top: 70px" @click="click2BeforNews"></span>
                        <ul>
                            <li class="new-show fl" v-for="(news ,index) in newsList.slice(start,end)" :key="index">
                                <div @click="click2ChooseNews(index)">
                                    <img :src="require('../../../ExGame-Asset/' + news.post_cover)">
                                    <div style="min-height: 30px;margin-top: 5px;width: 195px;">&nbsp;{{news.post_title}}</div>
                                    <div style="height: 20px;font-size: 12px">&nbsp;{{news.post_time}}</div>
                                </div>
                            </li>
                        </ul>
                        <span class="iconfont icon-iconfontjiantou5 fl" style="margin-left: 20px;margin-top: 70px" @click="click2NextNews"></span>
                    </div>
                </div>
                <div v-else style="text-align: center;line-height: 60px;font-size: 24px">
                    <div style="margin-top: 80px">
                        <svg xmlns="http://www.w3.org/2000/svg" width="200" height="200" fill="currentColor" class="bi bi-newspaper" viewBox="0 0 16 16">
                            <path d="M0 2.5A1.5 1.5 0 0 1 1.5 1h11A1.5 1.5 0 0 1 14 2.5v10.528c0 .3-.05.654-.238.972h.738a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 1 1 0v9a1.5 1.5 0 0 1-1.5 1.5H1.497A1.497 1.497 0 0 1 0 13.5v-11zM12 14c.37 0 .654-.211.853-.441.092-.106.147-.279.147-.531V2.5a.5.5 0 0 0-.5-.5h-11a.5.5 0 0 0-.5.5v11c0 .278.223.5.497.5H12z"/>
                            <path d="M2 3h10v2H2V3zm0 3h4v3H2V6zm0 4h4v1H2v-1zm0 2h4v1H2v-1zm5-6h2v1H7V6zm3 0h2v1h-2V6zM7 8h2v1H7V8zm3 0h2v1h-2V8zm-3 2h2v1H7v-1zm3 0h2v1h-2v-1zm-3 2h2v1H7v-1zm3 0h2v1h-2v-1z"/>
                        </svg>
                    </div>

                    当前游戏没有推送
                </div>
                <div class="clearbox" style="height: 20px"></div>
               <div class="game-config clearbox">
                    <div class="config-data">
                        <div style="margin-left: 30px;height: 60px;line-height: 60px;font-size: 24px">商品详情</div>
                        <div class="one-config fl">
                            <h2>最低配置要求</h2>
                            <table>
                                <tr>
                                    <td >内存</td>
                                    <td>{{ minimumConfiguration[0] }}</td>
                                </tr>
                                <tr>
                                    <td>显卡</td>
                                    <td>{{ minimumConfiguration[1] }}</td>
                                </tr>
                                <tr>
                                    <td>硬盘</td>
                                    <td>{{ minimumConfiguration[2] }}</td>
                                </tr>
                                <tr>
                                    <td>CPU</td>
                                    <td>{{ minimumConfiguration[3] }}</td>
                                </tr>
                                <tr>
                                    <td>系统</td>
                                    <td>{{ minimumConfiguration[4] }}</td>
                                </tr>
                            </table>
                        </div>
                        <div class="one-config fl">
                            <h2>推荐配置要求</h2>
                            <table>
                                <tr>
                                    <td >内存</td>
                                    <td>{{recommendedConfiguration[0]}}</td>
                                </tr>
                                <tr>
                                    <td>显卡</td>
                                    <td> {{recommendedConfiguration[1]}}</td>
                                </tr>
                                <tr>
                                    <td >硬盘</td>
                                    <td >{{recommendedConfiguration[2]}}</td>
                                </tr>
                                <tr>
                                    <td >CPU</td>
                                    <td >{{recommendedConfiguration[3]}}</td>
                                </tr>
                                <tr>
                                    <td>系统</td>
                                    <td>{{recommendedConfiguration[4]}}</td>
                                </tr>
                            </table>
                        </div>
                        <div class="one-config fl">
                            <h2>语言</h2>
                            <table>
                                <tr>
                                    <td>界面</td>
                                    <td><ul><li v-for="(item,inedx) in uiLanguage" :key="inedx" style="display: inline">{{item}} </li></ul></td>
                                </tr>
                                <tr>
                                    <td >完全音频</td>
                                    <td><ul><li v-for="(item,inedx) in soundLanguage" :key="inedx" style="display: inline">{{item}} </li></ul></td>
                                </tr>
                                <tr>
                                    <td>文本</td>
                                    <td ><ul><li v-for="(item,inedx) in textLanguage" :key="inedx" style="display: inline">{{item}} </li></ul></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
       <GamePageFooter/>
    </div>
</template>

<script>

import GamePageHeader from "@/views/GamePageHeader";
import GamePageFooter from "@/views/GamePageFooter";
export default {
    name: "GameBaseDetail",
components: {
        GamePageHeader,
GamePageFooter
    },
    data(){
        return{
            game_id:'',
            star:0,
            clicked:false,
            comment:'',
            start:0,
            showPos:0,
            end:4,
            gameName:'',
            isDownLoad:false,
            intro:'',
            newsList:[],
            minimumConfiguration:[],
            recommendedConfiguration:[],
            uiLanguage:[],
            soundLanguage:[],
            textLanguage:[],
        
        }
    },
    mounted() {

        this.game_id = this.$route.query.game_id
        this.isDownLoad = this.$route.query.is_download
        this.getConfigData(this.game_id)
        this.getGameName(this.$route.query.game_id)
        this.getGameIntro(this.$route.query.game_id)
        this.getGameNews(this.$route.query.game_id)
        setTimeout(this.modHeight,500)
    },
    methods:{
        click2NextNews(){
            if(this.end<this.newsList.length-1)
            {
                this.start++;
                this.end++;
            }
        },
       modHeight(){
            console.log('get----' + this.$refs.intro.offsetHeight)
             this.$refs["intro-img"].style.marginTop ="10px"
            if( this.$refs.intro.offsetHeight>125)
            {
                this.$refs["intro-img"].style.marginTop = ((this.$refs.intro.offsetHeight - 100)/2).toString() + 'px'
            }
            else
            {
                this.$refs["intro-img"].style.marginTop ="10px"
            }
        },
        click2BeforNews(){
            if(this.start>0)
            {
                this.start--;
                this.end--;
            }
        },
        click2ChooseNews(index){
            this.showPos=index
        },
        picStar(index){
            var star;
            for(let i=0;i<5;i++)
            {
                document.getElementById('star' + (i+1).toString()).style.color = 'rgb(231, 225, 205)'
            }
            for(let i=0;i<index;i++)
            {
                star = document.getElementById('star' + (i+1).toString())
                star.style.color = 'orange'
            }
            document.getElementById('star' + index.toString()).style.color = 'orange'
            this.clicked = false
            this.star =0
        },
        depicStar(index){

            if(this.clicked===false) {
                this.star =0
                for (let i = 0; i < 5; i++) {
                    index
                    document.getElementById('star' + (i + 1).toString()).style.color = 'rgb(231, 225, 205)'
                }
            }
        },
        click2ChooseStar(index){
            this.star = index
            this.clicked = true
            var star;
            for(let i=0;i<5;i++)
            {
                document.getElementById('star' + (i+1).toString()).style.color = 'rgb(231, 225, 205)'
            }
            for(let i=0;i<index;i++)
            {
                star = document.getElementById('star' + (i+1).toString())
                star.style.color = 'orange'
            }
            document.getElementById('star' + index.toString()).style.color = 'orange'
            this.star = index
        },
        getHight() { //
            let textArea = this.$refs.comment;
            if (textArea.scrollHeight > 52) {
                textArea.style.height = "52px";
                textArea.style.height = textArea.scrollHeight + "px";
            }
        },
        getGameNews(gid){
            this.$axios
                .post('api/library/getGameNews', {
                    game_id: gid,
                })
                .then(res => {

                    for(let i in res.data.gn) {
                        console.log(res.data.gn[i].post_cover)
                        console.log(res.data.gn[i].post_cover)
                        console.log(res.data.gn[i].post_title)
                        console.log(res.data.gn[i].content_text)
                        console.log(res.data.gn[i].post_time)
                        this.newsList.push({
                            post_cover: res.data.gn[i].post_cover,
                            post_title: res.data.gn[i].post_title,
                            post_time: res.data.gn[i].post_time,
                            content_text: res.data.gn[i].content_text
                        })
                    }
                })
                .catch(error => {
                    console.log(error)
                })
        },
       getConfigData:function (gid){
            var self = this;
            console.log('++++++')
            if(gid.length !== 0)
            {
                this.$axios.post('api/gamedetail/getGameConfigurations', {
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
                    for(i in res.data.minimum_configuration)
                    {
                        console.log('get   ' + res.data.minimum_configuration[i])
                        self.minimumConfiguration.push(res.data.minimum_configuration[i]) ;
                    }
                    for(i in res.data.recommended_configuration)
                    {
                        self.recommendedConfiguration.push(res.data.recommended_configuration[i]) ;
                    }
                    for(i in res.data.ui_language)
                    {
                        self.uiLanguage.push(res.data.ui_language[i]);
                    }
                    for(i in res.data.sound_language)
                    {
                        self.soundLanguage.push(res.data.sound_language[i]);
                    }
                    for(i in res.data.text_language)
                    {
                        self.textLanguage.push(res.data.text_language[i]);
                    }
                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏ID不能为空')
            }
        },
        getGameName(gid){
            if(gid.length !== 0)
            {
                this.$axios.post('api/gamedetail/getGameCarousel', {
                    game_id: gid
                }).then( res => {
                    switch(res.data.result){
                        case 1:
                            console.log("name 请求成功");
                            break;
                        default:
                            console.log('name 请求失败')
                            break;
                    }
                    this.gameName = res.data.game_name
                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏明不能为空')
            }
        },
        getGameIntro:function (gid){
            console.log('++++++ getAboutInfo')
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
                    console.log(res.data.about_game)
                    for(i in res.data.about_game)
                    {
                        this.intro = res.data.about_game[i].content;
                        break
                    }
                    this.dlcNum++
                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏ID不能为空')
            }
        },
        sendComment(uid,gid,rat,ct){
console.log(uid)
console.log(gid)
console.log(rat)
console.log(ct)
if(ct.length===0)
{
alert("请输入评论内容")
return
}
            if(gid.length !== 0)
            {
                this.$axios.post('api/library/comment', {
                    user_id:uid,
                    game_id: gid,
                    rating:rat,
                    content:ct
                }).then( res => {
                    console.log('++发布评论  ' + res.data.result)
	if(res.data.result===0)
{
alert("发布成功")
}
	if(res.data.result===-1)
{
alert("已发布的评论无法再修改")
}

                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏明不能为空')
            }
        },
        click2Send(rat,ct){
            this.sendComment(this.$store.state.userID,this.$route.query.game_id,rat,ct)
        },
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
*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}
ul, ol{
    list-style: none;
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
.canvas{

    background-size:100%;
    overflow: hidden;
    margin: auto;
    background: #2a3cad url(../assets/imgs/back.png);
    color: white;
}
.base-detail{
    width: 1420px;
    margin:auto;
    overflow: hidden;
    border-radius: 20px;
    border: 1px black solid;
    z-index:2;
    position:relative;
}
.base-detail-left{
    width: 450px;
    border-right: #666666 1px solid;
}
.base-detail-left img{
    width: 400px;
    border-radius: 10px;
    margin-top: 10px;
}
.go-detail{
    margin-top: 20px;
}
.go-column{
    margin-top: 20px;
}
.create-comment{
    font-size: 14px;
    margin-top: 200px;
}
.create-rate{
}
.create-description textarea{
    border-radius:5px;
    margin-top: 10px;
    background-color: #F5F5F5;
    width: 315px;
    margin-bottom: 10px;
    min-height: 52px;
    overflow-y: hidden;
    padding: 10px;
    resize: none;
    font-family: 'Microsoft YouYuan', sans-serif;
    font-size:14px;
}

.go-start{
    margin-top: 50px;
}
.go-start span{
    border-radius: 5px;
    width: 300px;
    background-color: #3B9CFF;
    color: white;
    font-weight: bolder;
    text-align: center;
    line-height: 50px;
    font-size: 20px;
    display: inline-block;
    height: 50px;
    margin-left: 50px;
}
.base-detail-right{
    width: 950px;
}
.lib-intro{
    min-height: 125px;
    overflow:hidden;
    margin-top: 10px;
    margin-left: 20px;
   line-height:25px;
    /*border-bottom: #e9e9e9 1px solid;*/
    border: silver 1px solid;
    border-radius: 10px;
}
.lib-intro:hover{
    background-color:rgb(211 ,211, 211, 0.2);
}
.lib-intro img{
    height: 100px;
    width:75px;
    margin-left: 10px;
    border-radius: 5px;
    box-shadow: 2px 2px 2px gray;
}
.new-show:hover{
    background-color:rgb(211 ,211, 211, 0.4);
}
.lib-game-intro{
    margin-top: 10px;
    font-size: 16px;
    margin-left: 20px;
    width:795px;
   height: 100px;
   overflow-y:auto;
}

.news-title{
    font-size: 30px;
    color: white;
    font-weight: bolder;
    margin-left: 20px;
    margin-top: 20px;
}
.news-content{
    font-size: 20px;
    color: white;
    margin-left: 20px;
    margin-top: 20px;
}
.news-board{
    margin-top: 10px;
    margin-left: 20px;
    height:295px;
    border-radius: 20px;
    overflow: hidden;
    background-color: #404040;

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
.board-info{
    width: 355px;
    overflow: auto;
    height:300px;
}
.board-img img{
    width: 550px;
    border-radius:10px;
}
.news-list{
    margin-top: 30px;
}
.new-show{
    width: 200px;
    margin-left: 20px;
    background-color: #212121;
    border-radius: 10px;
    overflow: hidden;
}
.new-show img{
    width: 200px;
}
.game-config{
    overflow: hidden;
    font-size: 16px;
    margin-top: 50px;
    line-height: 25px;
    float: bottom;
}

.one-config{
    width: 285px;
    height: 272px;
    margin-bottom: 20px;
    border-radius: 10px;
    margin-left: 15px;
}
.one-config h2{
    text-align: left;
    margin-left: 15px;
}

.one-config table{
    margin-top: 20px;
    margin-left: 15px;
    width: 246px;
    height: 136px;
    color: #F5F5F5;
    padding: 1px;
}
.one-config table tr td{
     height: 30px;
    border: 1.5px gray solid;
     display: table-cell;
     vertical-align: middle;
     font-size: 14px;
     text-align: center;
    min-width: 40px;
 }
</style>
