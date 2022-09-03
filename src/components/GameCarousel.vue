<!--张宇 1952168-->
<template>
    <div class="GameCarousel">
        <div>
            <div class="carousel-title">
                {{gameName}}
<!--                <router-link :to="{name:'GameColumn',params:{game_id:this.game_id}}">-->
                <router-link :to="{name:'GameColumn',query:{game_id:this.game_id}}">
                    <a>
                        <div class="to-column fr">
                            <span>前往论坛</span>
                        </div>
                    </a>
                </router-link>
            </div>

        </div>
        <div style="width: 1055px;color:black;margin: auto;border-radius: 10px;background-color: #e0e0e0">
            <div style="height: 20px"></div>
            <div class="carousel-body">
                <div class="m-ad clearbox">
                    <div class="m-ad-left fl">
                        <div class="m-ad-left-top" id="screen">
                            <video ref="main-player-1" v-show="this.mediaMap[anchorPos].srcType===1" src="" controls width="705" height="400" muted autoplay></video>
                            <img ref="main-player-2" v-show="this.mediaMap[anchorPos].srcType===2" src="" width="705" height="400" alt="">
                        </div>
                        <div class="m-ad-left-bottom">
                            <span class="iconfont icon-arrow-left fl" @click="click2Before()"></span>
                            <ul>
                                <li v-for="(temp,index) in showNum" :key="index">
                                    <!--                                <span>{{this.mediaMap[index].coverPath}}</span>-->
                                    <img class="media-list" v-if="launched===false" :ref="getID(index,'mask-')" :id="getID(index,'mask-')" :src="require('../assets/logo.png')" width="103" height="60" @click="click2Choose(index)">
                                    <img class="media-list" v-if="launched===true" :ref="getID(index,'mask-')" :id="getID(index,'mask-')" :src="require('../../../ExGame-Asset/' + mediaMap[index].coverPath)" width="103" height="60" @click="click2Choose(index)">
                                    <img class="play-button" v-if="mediaMap[index].srcType===1" :id="getID(index-1,'pbutton-')" src="../assets/play-button.jpg" width="20" height="20" @click="click2Choose(index)" >
                                </li>
                            </ul>
                            <span class="iconfont icon-iconfontjiantou5 fr" @click="click2Next()"></span>
                        </div>
                    </div>
                    <div class="m-ad-right fr">
                        <table >
                            <tr >
                                <td>发行日期</td>
                                <td >{{publishDate.slice(0,10)}}</td>
                            </tr>
                            <tr>
                                <td>开发商</td>
                                <td>{{developer}}</td>
                            </tr>
                            <tr >
                                <td>发行商</td>
                                <td>{{publisher}}</td>
                            </tr>
                            <tr >
                                <td>发行平台</td>
                                <td>{{publishPlat}}</td>
                            </tr>
                        </table>
                        <div class="tag-box" >
                            <ul>
                                <li v-for="(src,index) in tagList" :key="index">
                                    <p>{{ src }}</p>
                                </li>
                            </ul>
                        </div>
	<div style="height: 100px;line-height: 120px;text-align: center">
                            <span style="color: #666666;font-size: 24px">用户测评：</span>
                            <span style="font-size: 24px;font-weight: bolder;color:#ffd700" v-if="recRank===rankMap[0]">
                                {{recRank}}
                            </span>
                            <span style="font-size: 24px;font-weight: bolder;color:#00ffff"  v-if="recRank===rankMap[1]">
                                 {{recRank}}
                            </span>
                            <span style="font-size: 24px;font-weight: bolder;color:#00ffff"  v-if="recRank===rankMap[2]">
                                {{recRank}}
                            </span>
                            <span style="font-size: 24px;font-weight: bolder;color:#daa520" v-if="recRank===rankMap[3]">
                                 {{recRank}}
                            </span>
                            <span style="font-size: 24px;font-weight: bolder;color:#b22222" v-if="recRank===rankMap[4]">
                                {{recRank}}
                            </span>
                            <span style="font-size: 24px;font-weight: bolder;color:#b22222" v-if="recRank===rankMap[5]">
                                {{recRank}}
                            </span>
                            <span style="font-size: 24px;font-weight: bolder;color:#000000" v-if="recRank===rankMap[6]">
                                {{recRank}}
                            </span>
                            <span style="font-size: 24px;font-weight: bolder;color:#a9a9a9" v-if="recRank===rankMap[7]">
                                {{recRank}}
                            </span>
                        </div>
                       <div v-if="this.$store.state.isUser === 'User'">
                            <a v-if="published===false">即将上架</a>
                            <router-link :to="{name:'ShoppingCart',params:{user_id:this.game_id}}" v-else>
                            <a href="#" @click="click2Add()" >
                                加入购物车
                                <span v-if="discount!==1" style="text-decoration: line-through;color: #aaaaaa">￥{{price}}</span>
                                <span>&nbsp;￥{{price*discount}}</span>
                            </a>
                        </router-link>
	    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
export default {
    name: "GameCarousel",
    props:['game_id'],
    data() {
        return {
             recRank:'特别好评',
            rankMap:[
                '好评如潮',
                '特别好评',
                '多半好评',
                '褒贬不一',
                '特别差评',
                '多半差评',
                '差评如潮',
                '暂无评论',
            ],
            showNum: 6,
            launched:false,
            published:true,
            onPlay: 0,
            anchorPos:0,
            mediaStart:0,
            mediaEnd: 5,
            mediaMap: [
                {
                    index:0,
                    coverPath:'logo.png',
                    srcType:1,
                },
                {
                    index:1,
                    coverPath:'logo.png',
                    srcType:1,
                },
                {
                    index:2,
                    coverPath:'logo.png',
                    srcType:1,
                },
                {
                    index:3,
                    coverPath:'logo.png',
                    srcType:1,
                },
                {
                    index:4,
                    coverPath:'logo.png',
                    srcType:1,
                },
                {
                    index:5,
                    coverPath:'logo.png',
                    srcType:1,
                },
            ],
            sourceList:[],
            publishPlat:'臻Game',
            //下面是需要从后端获取的数据
            gameName:'',
            publishDate: '',
            developer: '',
            publisher: '',
            price: 0,
            discount:1,
            videoList:[],
            imageList:[],
            tagList:[],
        }
    },
    mounted() {
        console.log('+++++++++++++' + this.game_id)
        this.getInfo(this.game_id);
        // 用设置延时的方法解决异步
        setTimeout(this.fun,400)
    },
    methods:{
        tempF:function(){this.$router.push({name:'GameColumn',query:{game_id:this.game_id}})},
        getInfo:function (gid){
            var self = this;
            console.log('++++++')
            if(gid.length !== 0)
            {
                this.$axios.post('api/gamedetail/getGameCarousel', {
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
                    self.published = res.data.is_launched
                    self.gameName = res.data.game_name;
                    self.developer = res.data.developer;
                    self.publisher = res.data.publisher;
                    self.publishDate = res.data.publish_date;
                    self.price = res.data.price;
	    console.log('测评---' +res.data.rateResult )
                    self.recRank = res.data.rateResult;
                    console.log('打折' + res.data.discount)
                    self.discount = res.data.discount;
                    for(i in res.data.image_list)
                    {
                        console.log('get   ' + res.data.image_list[i])
                        self.imageList.push(res.data.image_list[i]) ;
                    }
                    for(i in res.data.video_list)
                    {
                        console.log('get   ' + res.data.video_list[i])
                        self.videoList.push(res.data.video_list[i]) ;
                    }
                    for(i in res.data.tag_list)
                    {
                        self.tagList.push(res.data.tag_list[i]) ;
                    }

                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏明不能为空')
            }
        },
        add2Cart:function (gid,op){
            if(gid===null)
            {
                alert('id 不能为空')
                return;
            }
            // var self = this;
            this.$axios.post('api/shopingcart/modifyUserShoppingCart', {
                user_id:this.$store.state.userID,
                game_id:gid,
                op_type:op
            }).then( res => {
                console.log('modify cart' + res.data.result)
            }).catch( err => {
                console.log(err);
            })
        },
        click2Add:function (){
          this.add2Cart(this.game_id,1)
        },
        init:function (){
            var that = this;
            var cut = this.videoList.length;
            console.log('video-length' + cut.toString())
            for(let i =0;i<this.imageList.length;i++)
            {
                this.sourceList.push({
                    index:i,
                    coverPath:that.imageList[i],
                    srcType:i<cut?1:2,
                })
            }
            this.showNum = this.sourceList.length>this.showNum?this.showNum:this.sourceList.length;
            this.mediaEnd = (this.sourceList.length>this.showNum?this.showNum:this.sourceList.length) - 1;
            for(let i=0;i<this.showNum;i++)
            {
                this.mediaMap[i].index = this.sourceList[i].index;
                this.mediaMap[i].coverPath = this.sourceList[i].coverPath;
                this.mediaMap[i].srcType = this.sourceList[i].srcType;
            }
        },
        fun:function (){
            this.click2Choose(0);
        },
        getSourcePath:function(index,type) {
            console.log(index.toString() + '   ' + type.toString())
            if(type===1) {
                console.log('get  ../assets/videos/ring-' + index.toString() + '.webm');
                return this.videoList[index-1];
            }
            else {
                console.log('get  ../assets/imgs/ring-' + index.toString() + '.jpg');
                return this.imageList[index-1];
            }
        },
        getID:function (index,pre){
            return pre  + index.toString();
        },
        click2Choose:function (pos){
            if(this.launched===false) {
                this.init();
                this.launched = true;
                console.log(this.imageList.length + '*')
                console.log(this.videoList.length + '*')
                console.log(this.showNum + '*')
                console.log(this.mediaEnd + '*')
            }
            console.log('choose = ' + pos.toString())
            this.onPlay = this.mediaMap[pos].index;
            this.anchorPos = pos;
            console.log('--------1');
            let mask = document.getElementById(this.getID(pos,'mask-'));
            console.log('--------2');
            for(let i = 0;i<this.showNum; i++)
            {
                document.getElementById(this.getID(i,'mask-')).style.border = '0px'
                console.log('--------' + (i+3).toString());
            }
            mask.style.border = '3px solid #eee'
            console.log('--------end');
            if(this.mediaMap[pos].srcType===1)
                this.$refs["main-player-1"].src = require('../../../ExGame-Asset/' + this.getSourcePath(this.onPlay + 1,this.mediaMap[pos].srcType));
            else
                this.$refs["main-player-2"].src = require('../../../ExGame-Asset/' + this.getSourcePath(this.onPlay + 1,this.mediaMap[pos].srcType));


        },
        click2Before:function (){
            if(this.onPlay===0)
            {
                return;
            }
            if(this.onPlay===this.mediaStart&&this.onPlay>=0)
            {
                this.mediaStart--;
                this.mediaEnd--;
                for(let i=0;i<this.showNum;i++)
                {
                    this.mediaMap[i].index = this.sourceList[this.mediaStart + i ].index;
                    this.mediaMap[i].coverPath = this.sourceList[this.mediaStart + i].coverPath;
                    this.mediaMap[i].srcType = this.sourceList[this.mediaStart + i].srcType;
                }
            }
            else {
                this.anchorPos--;
            }
            console.log('-pos ' + this.anchorPos.toString())
            console.log('type' + this.mediaMap[this.anchorPos].srcType.toString())
            if(this.mediaMap[this.anchorPos].srcType===1)
                this.$refs["main-player-1"].src = require('../../../ExGame-Asset/' + this.getSourcePath(this.onPlay,this.mediaMap[this.anchorPos].srcType));
            else
                this.$refs["main-player-2"].src = require('../../../ExGame-Asset/' + this.getSourcePath(this.onPlay,this.mediaMap[this.anchorPos].srcType));
            this.onPlay--;
            document.getElementById(this.getID(this.onPlay + 1  - this.mediaStart ,'mask-')).style.border = '0px'
            document.getElementById(this.getID(this.onPlay  - this.mediaStart  ,'mask-')).style.border ='3px solid #eee'
            console.log('go2before ' + this.onPlay.toString() );
        },
        click2Next:function (){
            if(this.onPlay===this.sourceList.length-1)
            {
                return;
            }
            if(this.onPlay >= this.showNum - 1 &&this.onPlay === this.mediaEnd)
            {
                this.mediaEnd++;
                this.mediaStart = this.mediaEnd - this.showNum + 1;
                for(let i=0;i<this.showNum;i++)
                {
                    this.mediaMap[i].index = this.sourceList[this.mediaStart + i].index;
                    this.mediaMap[i].coverPath = this.sourceList[this.mediaStart + i].coverPath;
                    this.mediaMap[i].srcType = this.sourceList[this.mediaStart + i].srcType;
                }
            }
            else{
                this.anchorPos++;
            }
            console.log('+pos ' + this.anchorPos.toString())
            console.log('type' + this.mediaMap[this.anchorPos].srcType.toString())
            if(this.mediaMap[this.anchorPos].srcType===1)
                this.$refs["main-player-1"].src = require('../../../ExGame-Asset/'+ this.getSourcePath(this.onPlay + 2,this.mediaMap[this.anchorPos].srcType));
            else
                this.$refs["main-player-2"].src = require('../../../ExGame-Asset/' + this.getSourcePath(this.onPlay + 2,this.mediaMap[this.anchorPos].srcType));
            console.log('----------')
            this.onPlay++;
            document.getElementById(this.getID(this.onPlay -1 - this.mediaStart ,'mask-')).style.border = '0px'
            document.getElementById(this.getID(this.onPlay -this.mediaStart,'mask-')).style.border ='3px solid #eee'
            console.log('go2next ' + this.onPlay.toString() );
        },
    }
}
</script>
<style scoped>
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
body::after{
    content: "";
    width: 100vw;
    height: 100vh;
    position: fixed;
    left: 0;
    top: 0;
    right: 0;
    z-index: -9999;
    pointer-events: none;
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
.to-column{
    font-size: 16px;
    height: 30px;
    width: 90px;
    line-height: 25px;
    font-weight: bolder;
    border-radius: 13px;
    border: white 2px solid;
    background-color: #eeeeee;
    /*color: #666666;*/
    text-align: center;
    position: relative;
    top:10px;
}
.carousel-body{
    width: 1005px;
    height: 505px;
    margin: 0 auto;
    border-radius: 15px;
}
.carousel-title{
    width: 1005px;
    height: 50px;
    margin: 0 auto;
    font-size: 36px;
    font-weight: bolder;
    color: #eeeeee;
    font-family: Arial, YouYuan, sans-serif;
}

.play-button{
    position: relative;
    top:-45px;
    left: 40px;
}
/*ad*/
.m-ad{
    width: 1005px;
    height: 475px;
    margin: 0 auto;
}
.m-ad-left{
    width: 705px;
    height: 400px;
}
.m-ad-left-top{
    width: 705px;
    height: 400px;
}
.m-ad-left-bottom{
    width: 705px;
    height: 75px;
    background-color: #212121;
}
.m-ad-left-bottom span{
    display: block;
    width: 30px;
    margin-top: 10px;
    font-size: 16px;
    text-align: center;
    height: 52px;
    line-height: 52px;
    color: gray;
}
.m-ad-left-bottom ul{
    float: left;
    width: 640px;
    height: 52px;
}
.m-ad-left-bottom ul li{
    float: left;
    width: 100px;
    height: 60px;
    margin-right: 8px;
    margin-top: 5px;

}
.m-ad-left-bottom ul li:last-of-type{
    margin-right: 0;
}

.m-ad-right{
    width: 300px;
    height: 475px;
    background-color: #fff;
}
.m-ad-right table{
    margin-left: 20px;
    margin-top: 20px;
    font-size: 12px;
    color: gray;
    border: 1px #fff solid;
    padding: 1px;
    width: 262px;
    height: 136px;
    background-color:#F0F0F0;
}
.m-ad-right table tr td{
    border: 1px #fff solid;
    padding: 1px;
    display: table-cell;
    vertical-align: middle;
    text-align: center;
}

.m-ad-right table tr td:last-of-type{
    padding-left: 10px;
}
.m-ad-right table tr:nth-of-type(3) td:last-of-type{
    color: black;
}
.m-ad-right table tr:nth-of-type(3) td:last-of-type::after{
    content: "";
    display: block;
    /*border-bottom: 2px dashed #ccc;*/
    width: 48px;

}
.tag-box{
    margin-left: 13px;
    height: 160px;
    color: black;
}
.m-ad-right p {
    width: 60px;
    height: 30px;
    line-height: 30px;
    text-align: center;
    font-size: 12px;
    border: 1px solid #ccc;
    float: left;
    margin-top: 10px;
    margin-left: 7px;
}
.m-ad-right a{
    clear: both;
    display: block;
    margin: 0 auto;
    width: 260px;
    height: 45px;
    color: white;
    font-size: 18px;
    text-align: center;
    line-height: 45px;
    font-weight: 900;
    background-color: #3B9CFF;
    border-radius: 10px;
}

</style>
