<template>
    <div class="canvas">
<game-page-header></game-page-header>
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
        <div style="width: 1055px;margin:100px auto;color:black;border-radius: 10px;background-color: #c0c0c0;overflow: hidden;position:relative" class="clearbox">
            <div>
                <div style="height: 60px"></div>
                <span class="column-name fl">{{gameName}}</span>
                <div class="column-head clearbox">
                    <div class="img-back">
		<router-link :to="{name:'GameDetail',query:{game_id:this.$route.query.game_id}}">
                        		<img  :src="require('../../../ExGame-Asset/Game/' +this.$route.query.game_id+ '/Cover/anCover.jpg')">
		</router-link>
                    </div>
                </div>
                <div style="
            text-align: center;
            width: 1005px;
            font-size: 20px;
            font-weight: bolder;
            margin:auto;
            background-color: white;
            height: 50px;
            border-radius: 0px 0px 10px 10px;">
                <span style="
                bottom: -15px;
                position: relative;
                border-bottom: red 4px solid;
                height: 30px;
                width: 100px;
                margin: auto;
                line-height: 10px;
                display:block;">
                    游戏论坛
                </span>
                </div>
            </div>
            <div class="dialog-wrapper">
                <el-dialog  :visible.sync="showBigImg" width="800px" append-to-body style="background-color: transparent">
                    <img  :src="bigImgSrc" width="800px" style="margin:auto">
                </el-dialog>
            </div>
            <div class="column-page clearbox">
                <ul class="column-left fl">
                    <ColumnCreator></ColumnCreator>
                    <li v-for="(column,index) in columnList" :key="index" class="m-column clearbox">
                        <div style="height: 10px"></div>
                        <div class="column-creator clearbox">
                            <div class="user-head fl">
                                <img :src="require('../../../ExGame-Asset/User/'+column.uid + '/ProfilePhoto.jpg')">
                            </div>
                            <div class="user-name fl">
                                {{column.name}}
                            </div>
                        </div>
                        <div class="column-title">{{picText(column.title)}}</div>
                        <div class="column-content">{{picText(column.content)}}</div>
                        <div style="margin-top: 10px;" v-show="showImg===index">
                            <div class="clearbox" style="width: 705px">
                                <div class="fl" style="width: 92px;height: 30px"></div>
                                <div class="op-buttons" style="margin-left: 10px">
                                <span @click="click2CloseImg">
                                    <svg  xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-dash-circle" viewBox="0 0 16 16">
                                        <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
                                        <path d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8z"/>
                                    </svg>
                                   <span style="position: relative;top:-3px"> 点击收起</span>
                                </span>
                                    <span style="margin-left: 20px" @click="click2Big">
                                     <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
                                        <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
                                        <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z"/>
                                    </svg>
                                    <span style="position: relative;top:-3px"> 查看大图</span>
                                </span>
                                </div>
                            </div>
                            <div id="arrow-l" class="fl" :style="arrowStyle">
                                <span class="iconfont icon-arrow-left"  @click="click2Before"></span>
                            </div>
                            <div class="show-board fl">
                                <img ref="img-board" id="board" :src="imgSrc" width="500px">
                            </div>
                            <div id="arrow-r" class="fr" :style="arrowStyle">
                                <span class="iconfont icon-iconfontjiantou5 " @click="click2Next"></span>
                            </div>
                            <div class="clearbox"  style="height: 20px"></div>
                        </div>
                        <div class="column-image clearbox" id="column-image">
                            <ul>
                                <li v-for="(img,i) in column.imageList" :key="i">
                                    <div class="m-image fl" @click=" click2ShowImg(index,i)" :id="getID2(index,i)">
                                        <img :src="require('../../../ExGame-Asset/' + img)">
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class="column-tips clearbox" @click="click2ShowReply(index)">
                            <span class="iconfont icon-pinglun fl"></span><span class="column-rpnum fl" >&nbsp;{{column.replyNum}}</span>
                            <span class="column-time fr">发布于：{{column.releaseTime}}</span>
                        </div>
                        <div :id="getID(index)" class="column-replies" style="display: none">
                            <ColumnReplier :column_id="column.columnID" style="margin-top: 10px;"></ColumnReplier>
                            <div v-if="column.replyNum ===0" style="height: 30px;text-align: center;line-height: 30px">还没有回帖</div>
                            <ul>
                                <li v-for="(reply,index) in columnReply" :key="index">
                                    <div class="m-reply">
                                        <div class="reply-creator clearbox">
                                            <div class="user-head-r fl">
                                                <img :src="require('../../../ExGame-Asset/User/'+reply.userID + '/ProfilePhoto.jpg')">
                                            </div>
                                            <div class="user-name-r fl">
                                                {{reply.name}}
                                            </div>
                                            <div class="reply-time fr">回复时间 {{reply.time}}&emsp;&emsp;&nbsp;&nbsp;&nbsp;</div>
                                        </div>
                                        <div class="reply-content">
                                            {{picText(reply.content)}}
                                        </div>
                                        <div style="height: 10px"></div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div style="height: 10px"></div>
                    </li>
                    <div class="more-comments clearbox" @click="getColumns(game_id,pgn)" v-show="this.isEnd===false">
                        <span > 查看更多帖子 <i class="icon-downarrow"></i></span>
                    </div>
                    <div class="end-comments clearbox" v-show="this.isEnd===true">
                        <span>已经到尽头了...</span>
                    </div>
                </ul>
                <div class="column-right fr">
                    <div class="clearbox" style="height: 60px;line-height: 60px;font-size: 24px">管理我的帖子</div>
                    <div class="my-columns clearbox">
                        <div style="height: 10px"></div>
                        <div style="margin-bottom: 15px;margin-top: 15px">
                            <span  @click="click2Switch(1)" id="cs1">我的发帖({{myColumnsNum}})</span>
                            <span  @click="click2Switch(2)" id="cs2">我的回复({{myRepliesNum}})</span>
                        </div>
                        <ul v-if="showColumn">
                            <li v-for="(colum,index) in myColumns" :key="index" class="my-column">
                                <div style="margin-left: 5px">
                                    <div class="my-column-title fl">{{picText(colum.title)}}</div>
                                    <span class="delete-column-button"  @click="click2ClickMyColumn(1,index)">删除</span>
                                    <div class="clearbox">
                                        <div class="my-column-content">{{picText(colum.content)}}</div>
                                        <div class="my-column-time">{{colum.releaseTime}}</div>
                                    </div>
                                </div>
                            </li>
                        </ul>
                        <ul v-else>
                            <li v-for="(reply,index) in myReplies" :key="index" class="my-reply">
                                <div style="margin-left: 5px">
                                    <div style="height: 5px"></div>
                                    <div style="min-height: 30px;width: 210px">
                                        <div class="my-reply-content fl">回复：{{picText(reply.content)}}</div>
                                        <span class="delete-column-button fr"  @click=" click2ClickMyColumn(2,index)">删除</span>
                                    </div>
                                    <div class="clearbox">
                                        <div class="my-reply-time">{{reply.releaseTime}}</div>
                                        <div class="my-reply-title">&nbsp;原贴：{{picText(reply.title)}}</div>
                                    </div>
                                    <div style="height: 10px"></div>
                                </div>
                            </li>
                        </ul>
                        <div class="more-my-column clearbox" v-if="(showColumn===true&&myColumnEnd===false)||(showColumn===false&&myColumnEnd===false)">
                            <span v-if="showColumn" @click="getMyColumns(this.$store.state.userID,1,myColumnPgn)"> 查看更多帖子 <i class="icon-downarrow"></i></span>
                            <span v-else @click="getMyColumns(this.$store.state.userID,2,myReplyPgn)"> 查看更多回复 <i class="icon-downarrow"></i></span>
                        </div>
                        <div class="end-my-column clearbox" v-else>
                            <span style="font-size: 12px">已经到尽头了...</span>
                        </div>

                        <div style="height: 10px"></div>
                    </div>

                    <div style="margin-top: 40px">
                        <p style="font-size: 18px;font-weight: bolder">意见反馈</p>
                        <p style="color: #666666;margin-top: 10px;margin-bottom: 30px">游戏的各种问题都将被解答</p>
                        <span style="margin-right:15px;display: inline-block;width: 80px;height: 20px;border:#666666 1px solid;border-radius: 10px">去反馈</span>
                        <span style="margin-left:15px;display: inline-block;width: 80px;height: 20px;border:#666666 1px solid;border-radius: 10px">历史反馈</span>
                    </div>
                    <div style="margin-top: 20px;color: #666666;">
                        <div style="line-height: 30px;height: 30px">臻Game官网</div>
                        <div  style="line-height: 30px;height:30px">www.exgame.com</div>
                        <div style="line-height: 30px;height: 30px">客服邮箱</div>
                        <div  style="line-height: 30px;height: 30px">2544668476@qq.com</div>
                        <div style="line-height: 30px;height: 30px">客服电话</div>
                        <div style="line-height: 30px;height: 30px">021-222222</div>
                    </div>
                    <div style="height: 50px"></div>
                </div>
            </div>
            <div class="clearbox" style="height: 20px"></div>
        </div>
     <GamePageFooter/>
    </div>
</template>

<script>
import ColumnReplier from "@/components/ColumnReplier";
import ColumnCreator from "@/components/ColumnCreator";
import GamePageHeader from "@/views/GamePageHeader";
import GamePageFooter from "@/components/GamePageFooter";
export default {
    name: "GameColumns",
    props:["game_id"],
    components:{
        ColumnCreator,
        ColumnReplier,
            GamePageHeader,
GamePageFooter
    },
    data(){
        return{
            faceList: [],
            gameName:'',
            gamePoster:'',
            showImg:'999',
            imgSrc:'',
            columnPos:0,
            imgPos:0,
            pgn:1,
            showBigImg:false,
            showColumn:true,
            bigImgSrc:'',
            arrowStyle:{
                height:'100px',
                lineHeight:'100px',
                width:'102px',
                textAlign:'center',
            },
            isReplyList:[],
            isEnd:false,
            columnList:[],
            columnReply:[],
            myColumnsNum:0,
            myColumnEnd:false,
            myColumnPgn:1,
            myColumns:[],
            myRepliesNum:0,
            myReplyEnd:false,
            myReplyPgn:1,
            myReplies:[],
        }
    },
    mounted() {
        const appData = require("../assets/emoji.json");
        for(let i in appData){
            this.faceList.push(appData[i].char);
        }
        this.getGameName(this.$route.query.game_id)
        this.getColumns(this.$route.query.game_id,this.pgn)
        this.getMyColumns(this.$store.state.userID,1,this.myColumnPgn)
        this.getMyColumns(this.$store.state.userID,2,this.myReplyPgn)
    },
    methods:{
        picText(tex)
        {
            var picedText = []
            // console.log(tex.length)
            // console.log( this.faceList.length)
            for(let i=0;i<tex.length;i++)
            {
                var num = ''
                // console.log(tex[i])
                // console.log(i)
                if(tex[i]==='@'&&tex[i+1]==='#'&&tex[i+5]==='#')
                {
                    num+=tex[i+2];
                    num+=tex[i+3];
                    num+=tex[i+4];
                    // console.log(tex[i+2] + tex[i+3]+ tex[i+4] + '-----')
                    // console.log(typeof parseInt(num))
                    // console.log(this.faceList[parseInt(num)])
                    // console.log('++++++')
                    picedText+=this.faceList[parseInt(num)]
                    i+=5
                }
                else{
                    picedText+=tex[i]
                }
            }
            return picedText
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
        getReply:function (cid){
            if(cid===null)
            {
                alert('id 不能为空')
                return;
            }
            // var self = this;
            this.columnReply=[];
            this.$axios.post('api/column/getGameColumnReplies', {
                column_id:cid
            }).then( res => {
                console.log('get reply' + res.data.message)
               for(let i in res.data.cr)
                {
                    console.log(res.data.cr[i].content)
                    console.log(res.data.cr[i].user_id)
                    this.columnReply.push({
                        name:res.data.cr[i].name,
                        userID:res.data.cr[i].user_id,
                        content:res.data.cr[i].content,
                        time:res.data.cr[i].release_time
                    })
                }
            }).catch( err => {
                console.log(err);
            })
        },
        getColumns:function (gid,pgn){
            if(gid===null)
            {
                alert('id 不能为空')
                return;
            }
            // var self = this;
            console.log('+++++')
            this.$axios.post('api/column/getGameColumns', {
                game_id:gid,
                page_num:pgn
            }).then( res => {
                console.log('get column' + res.data.result)
                this.isEnd = res.data.is_end
                this.pgn++
                for(let i in res.data.columnList)
                {
                    console.log('--------------------------')
                    // console.log(res.data.columnList[i])
                    // console.log(res.data.columnList[i].column_id)
                    // console.log(res.data.columnList[i].uid)
                    // console.log(res.data.columnList[i].name)
                    console.log(res.data.columnList[i].title)
                    // console.log(res.data.columnList[i].content)
                    console.log(res.data.columnList[i].reply_num)
                    // console.log(res.data.columnList[i].release_time)
                    this.isReplyList.push(false)
                    var temp = []
                    for(let j in res.data.columnList[i].image_list)
                    {
                        console.log( res.data.columnList[i].image_list[j])
                        temp.push(res.data.columnList[i].image_list[j])
                    }
                    this.columnList.push({
                        columnID:res.data.columnList[i].column_id,
                        uid:res.data.columnList[i].uid,
                        name:res.data.columnList[i].name,
                        title:res.data.columnList[i].title,
                        content:res.data.columnList[i].content,
                        imageList:temp,
                        replyNum:res.data.columnList[i].reply_num,
                        releaseTime:res.data.columnList[i].release_time,
                    })
                }
            }).catch( err => {
                console.log(err);
            })
        },
        getMyColumns:function (uid,tp,pgn){
            if(uid===null)
            {
                alert('id 不能为空')
                return;
            }
            // var self = this;
            console.log('+++++')
            this.$axios.post('api/column/getUserColumn', {
                user_id:uid,
                type:tp,
                page_num:pgn
            }).then( res => {
                console.log('get column' + res.data.result)

                if(tp===1)
                {
                    this.myColumnPgn++
                    this.myColumnEnd = res.data.is_end
                    this.myColumnsNum = res.data.column_num
                    for(let i in res.data.column_list)
                    {
                        // console.log('--------------------------')
                        // console.log(res.data.column_list[i])
                        // console.log(res.data.column_list[i].column_id)
                        // console.log(res.data.column_list[i].title)
                        // console.log(res.data.column_list[i].content)
                        // console.log(res.data.column_list[i].release_time)
                        this.myColumns.push({
                            columnID:res.data.column_list[i].column_id,
                            title:res.data.column_list[i].title,
                            content:res.data.column_list[i].content,
                            releaseTime:res.data.column_list[i].release_time,
                        })
                    }
                }
                else
                {
                    this.myReplyPgn++
                    this.myReplyEnd = res.data.is_end
                    this.myRepliesNum = res.data.column_num
                    for(let i in res.data.column_list)
                    {
                        // console.log('--------------------------')
                        // console.log(res.data.column_list[i])
                        // console.log(res.data.column_list[i].column_id)
                        // console.log(res.data.column_list[i].title)
                        // console.log(res.data.column_list[i].content)
                        // console.log(res.data.column_list[i].release_time)
                        this.myReplies.push({
                            columnID:res.data.column_list[i].column_id,
                            title:res.data.column_list[i].title,
                            content:res.data.column_list[i].content,
                            releaseTime:res.data.column_list[i].release_time,
                        })
                    }
                }

            }).catch( err => {
                console.log(err);
            })
        },
        deleteMyColumns:function (uid,cid,tp)
        {
            if(uid===null)
            {
                alert('id 不能为空')
                return;
            }
            // var self = this;
            console.log('+++++')
            this.$axios.post('api/column/deletUserColumn', {
                user_id:uid,
                column_id:cid,
                type:tp,
            }).then( res => {
                console.log('删除 成功 - ' + res.data.result)
            }).catch( err => {
                console.log(err);
            })
        },
        click2ClickMyColumn(tp,index){
            var temp=[]
            if(tp===1)
            {
                for(let i=0;i<this.myColumns.length;i++)
                {
                    if(i!==index)
                    {
                        temp.push(this.myColumns[i])
                    }
                }
                this.deleteMyColumns(this.$store.state.userID,this.myColumns[index].columnID,1)
                this.myColumns = temp
            }
            else {
                for(let i=0;i<this.myReplies.length;i++)
                {
                    if(i!==index)
                    {
                        temp.push(this.myReplies[i])
                    }
                }
                this.deleteMyColumns(this.$store.state.userID,this.myReplies[index].columnID,2)
                this.myReplies = temp
            }
        },
        click2Switch(tp){
            if(tp===1)
            {
                document.getElementById('cs1').style.borderBottom = '2px black solid'
                document.getElementById('cs2').style.borderBottom = 'none'
            }
            else
            {
                document.getElementById('cs2').style.borderBottom = '2px black solid'
                document.getElementById('cs1').style.borderBottom = 'none'
            }
            this.showColumn = !this.showColumn
        },
        getID(index){
            return 'rep-' + index.toString()
        },
        getID2(i,ii){
            // console.log(i.toString() + '-'+  ii.toString())
            return i.toString() + ii.toString()
        },
        click2CloseImg(){
            if(this.showImg===999)
            {
                return
            }
            this.showImg =999;
            for(let is=0;is<this.columnList.length;is++)
            {
                for(let index = 0;index< this.columnList[is].imageList.length;index++)
                {

                    var temp = document.getElementById(this.getID2(is,index))
                    temp.style.backgroundColor = 'black'
                    temp.style.opacity = '1'
                }
            }
        },
        click2Big(){
            this.showBigImg = true
            this.bigImgSrc = this.imgSrc
        },
        click2ShowImg(i,ii){
            console.log(i + '++++++++++++++' + ii)
            for(let is=0;is<this.columnList.length;is++)
            {
                for(let index = 0;index< this.columnList[is].imageList.length;index++)
                {
                    var temp = document.getElementById(this.getID2(is,index))
                    temp.style.backgroundColor = 'black'
                    temp.style.opacity = '1'
                }
            }
            this.showImg = i
            console.log(this.showImg[i])
            this.columnPos = i
            this.imgPos =ii
            this.imgSrc = require('../../../ExGame-Asset/'+ this.columnList[i].imageList[ii])
            this.chanagArrowHeight();
            for(let index = 0;index< this.columnList[i].imageList.length;index++)
            {
                temp = document.getElementById(this.getID2(i,index))
                temp.style.backgroundColor = 'black'
                temp.style.opacity = '0.3'
            }
            document.getElementById(this.getID2(i,ii)).style.backgroundColor = 'black'
            document.getElementById(this.getID2(i,ii)).style.opacity = '1'
        },
        chanagArrowHeight:function (){
            var that = this
            let h = 0;
            let img =new Image()
            img.src=that.imgSrc;
            img.onload=function () {
                // console.log('图片原始高度', img.height)
                // console.log('图片原始宽度',img.width)
                // console.log('图片原始宽度',h)
                h = 500/img.width*img.height;
                that.arrowStyle.height = h.toString() + 'px'
                that.arrowStyle.lineHeight = h.toString() + 'px'
            }
        },
        click2Next:function (){
            var that = this
            this.chanagArrowHeight();
            if( that.imgPos+1< that.columnList[that.columnPos].imageList.length)
            {

                that.imgSrc = require('../../../ExGame-Asset/' + that.columnList[ that.columnPos].imageList[ that.imgPos+1])
                that.imgPos++;
                for(let index = 0;index<that.columnList[that.columnPos].imageList.length;index++)
                {
                    console.log(index)
                    document.getElementById(this.getID2(this.columnPos,index)).style.backgroundColor = 'black'
                    document.getElementById(this.getID2(this.columnPos,index)).style.opacity = '0.3'
                }
                document.getElementById(this.getID2(this.columnPos,that.imgPos)).style.backgroundColor = 'black'
                document.getElementById(this.getID2(this.columnPos,that.imgPos)).style.opacity = '1'

            }
        },
        click2Before:function (){
            var  that = this;
            this.chanagArrowHeight();
            if( that.imgPos>0&&that.imgPos<that.columnList[that.columnPos].imageList.length)
            {
                that.imgSrc = require('../../../ExGame-Asset/'+  that.columnList[ that.columnPos].imageList[ that.imgPos-1])
                that.imgPos--;
                for(let index = 0;index<that.columnList[that.columnPos].imageList.length;index++)
                {
                    document.getElementById(this.getID2(this.columnPos,index)).style.backgroundColor = 'black'
                    document.getElementById(this.getID2(this.columnPos,index)).style.opacity = '0.3'
                }
                document.getElementById(this.getID2(this.columnPos,that.imgPos)).style.backgroundColor = 'black'
                document.getElementById(this.getID2(this.columnPos,that.imgPos)).style.opacity = '1'
            }
        },
        click2ShowReply:function (index){
            var reply = document.getElementById(this.getID(index))
            if(reply.style.display === 'none')
            {
                reply.style.display = 'block'
            }
            else
            {
                reply.style.display = 'none'
            }
            if(this.isReplyList[index]===true)
            {
                this.isReplyList[index] = false
                return
            }
            console.log(index)
            this.isReplyList[index] = true
            this.getReply(this.columnList[index].columnID)
            for(let i=0;i<this.columnList.length;i++)
            {
                if(i!==index)
                    document.getElementById(this.getID(i)).style.display = 'none'
            }

        },
    }
}
</script>

<style scoped>
#particles-js {
  width: 100%;
  height: 100%;
  position: absolute;
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
.canvas {
    position:fixed;
    width:100%;
    height:100vh;
    background-color: black;
    overflow: auto;
    color: black;
}

.my-columns{
    width: 250px;
    min-height: 50px;
    background-color: #e7e1cd;
    margin: auto auto 10px;
    border-radius: 10px;
}
.delete-column-button{
    height: 30px;
    line-height: 30px;
    font-size: 12px;
    text-decoration: underline;
}
.delete-column-button:hover{
    color: red;
}
.my-column{
    width: 220px;
    margin: 5px auto;
    border-radius: 5px;
    background-color: white;
}
.my-column-title{
    width: 175px;
    font-weight: bolder;
    text-align: left;
    height: 30px;
    line-height: 30px;
}
.my-column-content{
    text-align: left;
    word-wrap: break-word;
    margin-left: 10px;
    margin-bottom: 10px;
    margin-top: 5px;
    font-size: 14px;
    width: 190px;
}
.my-column-time{
    height: 20px;
    line-height: 20px;
    font-size: 12px;
    text-align: left;
}
.my-reply{
    width: 220px;
    margin: 5px auto;
    border-radius: 5px;
    background-color: white;
}
.my-reply-content{
    width: 180px;
    text-align: left;
    line-height: 25px;
    font-size: 14px;
}
.my-reply-time{
    height: 25px;
    line-height: 25px;
    font-size: 10px;
    text-align: left;
    margin-top: 5px;
}
.my-reply-title{
    margin: 5px auto auto;
    height: 30px;
    line-height: 30px;
    width: 195px;
    font-size: 14px;
    background-color: #eeeeee;
    border-radius: 5px;
    text-align: left;
}
#cs1{
    margin-right: 10px;
    border-bottom: 2px black solid;
}
#cs2{
    margin-left: 10px;
}
.more-my-column
{
    height: 30px;
    line-height: 30px;
    text-align: center;
    font-size: 12px;
    font-weight: bolder;
    color: #666666;
    background-color: rgb(240, 240, 240 , 50%);
    border-radius: 5px;
    width: 200px;
    margin: 15px auto;
}
.more-my-column:hover{
    background-color: rgb(240, 240, 240 );
}
::v-deep .el-dialog__header {
    padding: 0;
    width: 100%;
    height: 0;
}
::v-deep .el-dialog__footer {
    padding: 0;
    width: 100%;
    height: 0;
}
::v-deep .el-dialog__body {
    padding: 0 0 0 0;
}
.column-head{
    background-color: white;
    width: 1005px;
    border-radius: 10px 10px 0px 0px;
    height: 50px;
    margin: 120px auto 0px;
}
.column-head img{
    /*position: relative;*/
    height: 160px;
    /*margin-left: 50px;*/
    /*top: -110px;*/
    border-radius: 10px;
    margin-top: 9px;
    margin-left: 10px;
}
.img-back{
    position: relative;
    height: 180px;
    width: 142px;
    margin-left: 50px;
    top: -120px;
    border-radius: 10px;
    background-color: white;
    border: #666666 1px solid;
    box-shadow: 8px 8px 10px #888888;
}
.column-name{
    margin-left: 300px;
    margin-top: 20px;
    line-height: 120px;
    font-size: 36px;
    font-weight: bolder;
}
.column-page{
    width: 1005px;
    margin:  auto;
}
.column-left{
    width:705px;
    margin-top: 5px;
}
.column-right{
    width: 285px;
    background-color: white;
    /*height: 200px;*/
    min-height: 100vh;
    margin-top: 15px;
    border-radius: 10px;
    text-align: center;
}
.column-creator{
    height: 35px;
    margin-left: 15px;
    line-height: 30px;
}
.column-creator .user-head{
    overflow: hidden;
    border-radius: 50%;
    height: 30px;
    width: 30px;
    margin-top: 2px;
}
.column-creator .user-name{
    font-size: 16px;
    font-weight: bolder;
    margin-left: 10px;
}
.column-creator img{
    height: 30px;
    width: 30px;
}
.show-board{
    width: 500px;
    margin: auto;
    border-radius: 0 0 10px 10px;
    overflow: hidden;
}
.op-buttons{
    width: 500px;
    display: inline-block;
    height: 30px;
    line-height: 35px;
    border-radius: 10px 10px 0 0 ;
    background-color: #eeeeee;
    font-size: 12px;
}
.column-replies{
    border-top: #666666 1px solid;
}
.m-reply{
    border-top: #e0e0e0 1px solid;
    border-bottom: #e0e0e0 1px solid;
}
.reply-creator{
    height: 30px;
    margin-left: 15px;
    line-height: 30px;
    margin-top: 20px;
}
.reply-creator .user-head-r{
    overflow: hidden;
    border-radius: 50%;
    height: 25px;
    width: 25px;
    margin-top: 2px;
}
.reply-creator .user-name-r{
    font-size: 14px;
    font-weight: bolder;
    margin-left: 10px;
    line-height: 15px;
}
.reply-creator img{
    height: 25px;
    width: 25px;
}
.reply-content{
    margin-left: 55px;
    font-size: 14px;
    word-wrap: break-word;
}
.reply-time{
    font-size: 12px;
    color: #666666;
    line-height: 15px;
}
.m-column{
    background-color: white;
    border-radius: 10px;
    margin-top: 10px;
    margin-bottom: 10px;
}

.column-title{
    font-weight: bolder;
    font-size: 18px;
    margin-left: 30px;
    margin-top: 10px;
    width: 685px;
    /*border: red 1px solid;*/
}
.column-content{
    margin-left: 30px;
    width: 685px;
    margin-top: 10px;
    font-size: 14px;
    color: #666666;
    /*border: red 1px solid;*/
}
.column-image{
    margin-top: 20px;
    width: 520px;
    margin-left:30px;
}
.m-image{
    overflow: hidden;
    width: 160px;
    height: 160px;
    margin-left: 5px;
    margin-right: 5px;
    margin-bottom: 10px;
    border-radius: 5px;
}
.m-image img{
    width: 160px;
    height: 160px;
    object-fit: cover;
}
.column-tips{
    margin-left: 30px;
    height: 30px;
    width: 660px;
    line-height: 30px;
    font-size: 14px;
}
.column-replies{
    margin-left: 10px;
    width: 685px;
    margin-top: 10px;
}
.more-comments{
    height: 40px;
    line-height: 40px;
    text-align: center;
    font-size: 14px;
    font-weight: bolder;
    color: #666666;
    background-color: rgb(240, 240, 240 , 50%);
    border-radius: 5px;
    width: 700px;
    margin: auto auto 50px auto;
}
.end-comments{
    height: 40px;
    line-height: 40px;
    text-align: center;
    font-size: 14px;
    font-weight: bolder;
    color: #666666;
    border-radius: 5px;
    width: 700px;
    margin: auto auto 20px auto;
}

.more-comments:hover{
    background-color: rgb(240, 240, 240 );
}
.icon-downarrow{
    display:inline-block;
    width:6px;
    height:6px;
    border-left:2px solid black;
    border-bottom:2px solid black;
    transform:rotate(-45deg);
    margin:0 0 4px 6px;
}
</style>
