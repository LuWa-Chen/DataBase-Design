<!--张宇 1952168-->
<template>
    <div class="GameCarousel">
        <div class="carousel-title">
            {{gameName}}
        </div>
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
                                <img class="media-list" v-if="launched===false" :ref="getID(index,'mask-')" :id="getID(index,'mask-')" :src="require('../../../ExGame-Asset/Game/0000000006/Exhibition/Photo/' +'logo.png')" width="103" height="60" @click="click2Choose(index)">
                                <img class="media-list" v-if="launched===true" :ref="getID(index,'mask-')" :id="getID(index,'mask-')" :src="require('../../../ExGame-Asset/Game/' + mediaMap[index].coverPath)" width="103" height="60" @click="click2Choose(index)">
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
                    <a href="#">加入购物车 ￥{{price*(100-discount)/100}}<span v-if="discount!==0" style="color: red">  {{ discount }}%off</span></a>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
export default {
    name: "TEST",
    props:['game_id'],
    data() {
        return {
            basic_source_path:"../../../ExGame-Asset/Game/",
            showNum: 6,
            launched:false,
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
            discount:100,
            videoList:[],
            imageList:[],
            tagList:[],
        }
    },
    mounted() {
        this.getInfo(this.game_id);
        // 用设置延时的方法解决异步
        setTimeout(this.fun,200)
    },
    methods:{
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
                    self.gameName = res.data.game_name;
                    self.developer = res.data.developer;
                    self.publisher = res.data.publisher;
                    self.publishDate = res.data.publish_date;
                    self.price = res.data.price;
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
        init:function (){
            var that = this;
            var cut = this.videoList.length;
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
            this.discount = 50;
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
                this.$refs["main-player-1"].src = require('../../../ExGame-Asset/Game/' + this.getSourcePath(this.onPlay + 1,this.mediaMap[pos].srcType));
            else
                this.$refs["main-player-2"].src = require('../../../ExGame-Asset/Game/' + this.getSourcePath(this.onPlay + 1,this.mediaMap[pos].srcType));

            if(this.launched===false) {
                this.init();
                this.launched = true;
                console.log(this.imageList.length + '*')
                console.log(this.videoList.length + '*')
                console.log(this.showNum + '*')
                console.log(this.mediaEnd + '*')
            }
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
                this.$refs["main-player-1"].src = require('../../../ExGame-Asset/Game/' + this.getSourcePath(this.onPlay,this.mediaMap[this.anchorPos].srcType));
            else
                this.$refs["main-player-2"].src = require('../../../ExGame-Asset/Game/' + this.getSourcePath(this.onPlay,this.mediaMap[this.anchorPos].srcType));
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
                this.$refs["main-player-1"].src = require('../../../ExGame-Asset/Game/'+ this.getSourcePath(this.onPlay + 2,this.mediaMap[this.anchorPos].srcType));
            else
                this.$refs["main-player-2"].src = require('../../../ExGame-Asset/Game/' + this.getSourcePath(this.onPlay + 2,this.mediaMap[this.anchorPos].srcType));
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
.carousel-body{
    width: 1005px;
    height: 525px;
    margin: 0 auto;
}
.carousel-title{
    width: 1005px;
    height: 50px;
    margin: 0 auto;
    font-size: 36px;
    font-weight: bolder;
    color: black;
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
    border-bottom: 2px dashed #ccc;
    width: 48px;

}
.tag-box{
    margin-left: 13px;
    height: 260px;
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
    font-size: 20px;
    text-align: center;
    line-height: 45px;
    font-weight: 900;
    background-color: #3B9CFF;
}

</style>
