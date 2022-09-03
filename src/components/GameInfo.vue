<!--1952168 张宇-->
<template>
    <div style="width: 1055px;min-height:800px;margin: auto;border-radius: 10px;background-color: #e0e0e0;overflow: hidden" class="clearbox">
        <div class="m-content clearbox">
            <!--			应用特性-->
            <div class="m-content-right fr">
                <!--                <div class="panel">-->
                <!--                    <h2>应用特性</h2>-->
                <!--                    <table>-->
                <!--                        <tr>-->
                <!--                            <td><span class="iconfont icon-gouwucheman"></span></td>-->
                <!--                            <td>{{ gameFeatures[0] }}</td>-->
                <!--                        </tr>-->
                <!--                        <tr>-->
                <!--                            <td><span class="iconfont icon-duoren"></span></td>-->
                <!--                            <td>{{ gameFeatures[1] }}</td>-->
                <!--                        </tr>-->
                <!--                        <tr>-->
                <!--                            <td><span class="iconfont icon-youxiguanli"></span></td>-->
                <!--                            <td>{{ gameFeatures[2] }}</td>-->
                <!--                        </tr>-->
                <!--                        <tr>-->
                <!--                            <td><span class="iconfont icon-bi"></span></td>-->
                <!--                            <td>{{ gameFeatures[3] }}</td>-->
                <!--                        </tr>-->
                <!--                        <tr>-->
                <!--                            <td><span class="iconfont icon-lianjiezhuangtai"></span></td>-->
                <!--                            <td>{{ gameFeatures[4] }}</td>-->
                <!--                        </tr>-->
                <!--                    </table>-->
                <!--                </div>-->
                <!--				配置需求-->
                <div class="panel">
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
                <div class="panel">
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
                <!--				语言-->
                <div class="panel">
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
            <div class="m-content-left fl">
                <div class="m-tt fl">游戏拓展包</div>
                <ul  v-if="dlcList.length===dlcNum">
                    <li v-for="(dlc,index) in dlcList.length" :key="index" >
                        <div class="m-hot clearbox" ref="dlcwin">
		<router-link :to="{name:'GameDetail',query:{game_id:dlcList[index]}}">
                            <div class="m-hot-left fl" >
                                    <img class="hot-img" ref="dlcimg" id="dlcimg" :src="require('../../../ExGame-Asset/'+ dlcIntro[index].poster)" >
                            </div>
                                </router-link>
                            <div class="m-hot-right fr" @change="modDlcHeight">
                                <div class="dlc-content">
                                    <p class="dlc-name" @click="modDlcHeight">{{ dlcInfo[index].dlcName }}<p>
                                   <p class="dlc-intro">{{ dlcIntro[index].content }}</p>
                                </div>
                                <div>
                                    <div class="dlc-publish-time">发布于 {{ dlcInfo[index].dlcPublishDate }}</div>
                                    <router-link :to="{name:'ShoppingCart',params:{user_id:'000000001'}}">
                                        <div class="dlc-add" @click="click2Cart(dlcList[index])">加入购物车
                                            <span v-if="dlcInfo[index].dlcDiscount!==1" style="text-decoration: line-through;color: #aaaaaa">￥{{ dlcInfo[index].dlcPrice}}</span>
                                            <span>&nbsp;￥{{ dlcInfo[index].dlcPrice * dlcInfo[index].dlcDiscount}}</span>
                                        </div>
                                    </router-link>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
                <div  v-if="dlcList.length===0" class="clearbox"
                      style="height: 60px;line-height: 30px;margin-left: 30px;font-weight: bolder;font-size: 18px;color: #666666"
                >
                    当前游戏没有拓展包</div>
                <div class="m-tt fl">关于这款游戏</div>
                <div class="m-introduction" style="min-height: 200px">
                    <ul v-if="launchedIntro">
                        <li v-for="(intro,index) in aboutGame" :key="index">
                            <img class='game-poster' :src="require('../../../ExGame-Asset/Game/' +game_id + '/Cover/Cover.jpg')" alt="">
                            <div class="about-title">{{intro.title}}</div>
                            <div class="about-content">{{intro.content}}</div>
                            <div style="height: 20px"></div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div style="height: 20px" class="clearbox"></div>
    </div>
</template>

<script>
export default {
    name: "GameInfo",
    props:['game_id'],
    data() {
        return {
            uid:this.$store.state.userID,
            launchedIntro:false,
            launchedDLC:false,
            aboutGame:[],
            dlcInfo:[],
            dlcIntro:[],
            dlcList:[],
            gameFeatures:[],
            minimumConfiguration:[],
            recommendedConfiguration:[],
            uiLanguage:[],
            soundLanguage:[],
            textLanguage:[],
            dlcNum:0
        }
    },
    mounted() {
        setTimeout(this.fun,600)
    },
    methods:{
        fun(){
            this.getConfigData(this.game_id);
            this.getAboutData(this.game_id);
            setTimeout(this.getDLC,600)
        },
         modDlcHeight() {
            var boxHeight = []
            console.log('modify-height')
            for(let i in this.$refs.dlcwin)
            {
                console.log(this.$refs.dlcwin[i].offsetHeight)
                boxHeight.push( this.$refs.dlcwin[i].offsetHeight)
            }
            for(let i in this.$refs.dlcimg)
            {
                console.log( this.$refs.dlcimg[i].offsetHeight)
                console.log(((boxHeight[i] - this.$refs.dlcimg[i].offsetHeight)/2).toString() + 'px')
                this.$refs.dlcimg[i].style.marginTop = ((boxHeight[i] - this.$refs.dlcimg[i].offsetHeight)/2).toString() + 'px'
                //this.$refs.dlcimg[i].style.marginTop ='89px'
                console.log( this.$refs.dlcimg[i].offsetHeight)
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
        click2Cart(id){
            console.log('++++++++++++++++++++++++++++++++')
            this.add2Cart(id,1)
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
                    for(i in res.data.game_features)
                    {
                        console.log('get   ' + res.data.game_features[i])
                        self.gameFeatures.push(res.data.game_features[i]) ;
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
        getAboutData:function (gid){
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
                            console.log("Into 请求成功");
                            break;
                        default:
                            console.log('Into 请求失败')
                            break;
                    }
                    console.log(res.data.about_game)
                    for(i in res.data.about_game)
                    {
                        console.log('get  intro title ' + res.data.about_game[i].title)
                        console.log('get  intro content ' + res.data.about_game[i].content)
                        // var path = res.data.about_game[i].poster + '.gif'
                         self.aboutGame.push({
                            poster:res.data.about_game[i].poster,
                            title:res.data.about_game[i].title,
                            content: res.data.about_game[i].content,
                        }) ;
                    }
                    console.log('dlc-list' + res.data.dlc_list)
                    for(i in res.data.dlc_list)
                    {
                        console.log('get  dlc ++++++ ' + res.data.dlc_list[i])
                        self.dlcList.push(res.data.dlc_list[i]) ;
                    }
                    this.launchedIntro = true
                    // console.log('dlc' + '0000000006')
                    // self.dlcList.push('0000000006')
                }).catch( err => {
                    console.log(err);
                })
            }
            else{
                alert('游戏ID不能为空')
            }

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
                            console.log("GameCarousel 请求成功");
                            break;
                        default:
                            console.log('GameCarousel 请求失败')
                            break;
                    }
                    console.log(res.data.game_name)
                    console.log(res.data.publish_date)
                    console.log(res.data.price)
                    console.log(res.data.discount)
                    self.dlcInfo.push(
                        {
                            dlcName:res.data.game_name,
                            dlcPublishDate:res.data.publish_date,
                            dlcPrice:res.data.price,
                            dlcDiscount:res.data.discount,
                        }
                    )
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
                    
                    for(i in res.data.about_game)
                    {    
                         console.log("dlc-content    + " + res.data.about_game[i].content)
                         self.dlcIntro.push({
                            isPub : res.data.about_game[i].is_launched,
                            poster:res.data.about_game[i].poster,
                            content: res.data.about_game[i].title,
                        }) ;
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
        getDLC:function (){
            for(let i in this.dlcList) {
                console.log('get dlc -info' + this.dlcList[i]);
                var self = this
                setTimeout(function(){
                    self.getGameInfo(self.dlcList[i])
                    self.getAboutInfo(self.dlcList[i])
                },200)
            }
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
/*content*/
.m-content{
    width: 1005px;
    margin: 0 auto;
    margin-top: 20px;
    margin-bottom: 50px;
    color:black;
}

.m-content-left{
    width: 705px;
}
.game-poster{
    width: 100%;
    border-radius: 10px;
    margin-bottom: 10px;
}
.about-title{
    white-space: pre-wrap;   /*这是重点。文本换行*/
    margin-bottom: 20px;
    font-size: 22px;
    line-height:40px;
    margin-left: 10px;
}
.about-content{
    margin-left: 10px;
    line-height:25px;
    white-space: pre-wrap;   /*这是重点。文本换行*/
    min-height: 200px;
   color:#666666;
}
.m-tt{
    font-weight: normal;
    margin-bottom: 20px;
    margin-top: 20px;
    font-size: 20px;
    color: black;
}
.m-evaluation{
    width:703px;
    /*height:305px;*/
    height: auto;
    background-color: #fff;
    overflow: hidden;
    position: relative;
    /*border: black 2px solid;*/
}
.m-introduction{
    width:703px;
    /*height:305px;*/
    height: auto;
    background-color: transparent;
    overflow: hidden;
    position: relative;
    border-radius: 10px;
color:black;
    /*border: black 2px solid;*/
}
.m-introduction p{
    margin-top: 10px;
    margin-left: 10px;
    margin-right: 10px;
    font-size: 14px;
    text-align: left;
}

.m-introduction h4{
    margin-left: 10px;
    margin-top: 20px;
    font-size: 14px;
    text-align: left;
}

.m-introduction span:last-of-type{
    position: absolute;
    left:385px;
    bottom: 0;
    color: black;
    font-weight: 900;
    font-size: 10px;
    z-index: 999;
}
.m-content-right{
    /*height: 1200px;*/
    width: 285px;
}

.panel{
    width: 285px;
    height: 272px;
    background-color: #fff;
    margin-bottom: 20px;
    border-radius: 10px;
    /*border: black 2px solid;*/
}
.panel h2{
    padding-top: 20px;
    padding-left: 20px;
    font-weight: normal;
}

.panel table{
    margin-top: 20px;
    margin-left: 15px;
    width: 246px;
    height: 136px;
    color: gray;
    border: 1px #fff solid;
    padding: 1px;
    background-color:#F0F0F0;
}

.panel table tr td{
    height: 30px;
    border: 1px #fff solid;
    padding: 1px;
    display: table-cell;
    vertical-align: middle;
    font-size: 12px;
    text-align: center;
}
.panel:first-of-type table tr td span{
    color: #F9B154;
}
.panel:first-of-type table tr td:last-of-type{
    color: gray;
}
.panel:nth-of-type(2) table tr td:first-of-type{
    width: 80px;
    height: 30px;
}
.panel:nth-of-type(2) table tr td:last-of-type{
    width: 140px;
    height: 30px;
}
.panel:nth-of-type(3) table tr td:first-of-type{
    width: 80px;
    height: 30px;
}
.panel:nth-of-type(3) table tr td:last-of-type{
    width: 140px;
    height: 30px;
}

.panel table tr td:last-of-type{
    padding-left: 10px;
}
.panel:nth-of-type(4){
    height:230px;
}
.panel:nth-of-type(4) table tr td:first-of-type{
    color: gray;
}
/*更新与活动*/
.m-hot{
    width: 703px;
    min-height: 148px;
    background-color: #fff;
    margin-bottom: 20px;
    border-radius: 10px;
    overflow: hidden;
    /*border: black 1px solid;*/
}
.m-hot-left{
    height: 148px;
}
.hot-img{
    width: 200px;
    border-radius: 5px;
    margin-top: 30px;
    margin-left:5px;
}
.m-hot-left a{
    display: block;
    width:161px;
    height:107px;
    margin-left: 10px;
    margin-top: 10px;
}
.m-hot-right{
    min-height: 148px;
    width: 485px;
    border: black 1px solid;
    border-radius: 5px;
}

.dlc-name{
    font-size: 20px;
    font-weight: bolder;
    margin-bottom: 10px;
    text-align: left;
}
.dlc-intro{
    text-align: left;
    white-space: pre-wrap;
    height: 90px;
    line-height: 18px;
    overflow: hidden;
    color: #666666;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 5;
    -webkit-box-orient: vertical;
}
.dlc-content{
    min-height: 105px;
    margin-top: 10px;
    margin-left: 10px;
}
.dlc-publish-time{
    height: 20px;
    width: 200px;
    margin-left: 10px;
    display: block;
    margin-top: 10px;
    float: left;
    font-size: 14px;
}
.dlc-add{
    /*border: #000000 1px solid;*/
    width: 200px;
    height: 20px;
    float: right;
    margin-top: 10px;
    margin-right: 10px;
    text-align: center;
    line-height: 20px;
    color: white;
    font-size: 14px;
    font-weight: bold;
    border-radius: 5px;
    background-color: #3B9CFF;
}
</style>
