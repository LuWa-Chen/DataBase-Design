<template>

    <div class="g-bd"> <h1>564+6+</h1><p>+++</p>
        <div class="m-content clearbox">
            <div class="m-content-left fl">
                <!--				游戏详情-->
                <h2 class="m-tt">游戏详情</h2>
                <div class="m-introduction" ref="m-introduction">
                    <h3 class="intro-title">{{introDetail.introTitle}}</h3>
                    <p class="intro-sum">
                        {{introDetail.introSum}}
                    </p>
<!--                    <a href="#"><img class='game-poster' src="../assets/imgs/ring.jpg" alt=""></a>-->
                    <ul>
                        <li v-for="(detail,index) in introDetail.detailList" :key="index">
                            {{detail.content.title}}<br/>
                            {{detail.content.content}}
                        </li>
                    </ul>
                    <span class="u-hide" @click="click2Show()">
					展开全部
					</span>
                    <span class="iconfont icon-jiantou9"  @click="click2Show()"></span>
                </div>

                <!--				更新活动-->
                <h2 class="m-tt fl">更新与活动</h2>
                <a href="#" class="fr u-tips">查看全部</a>
                <ul>
                    <li v-for="(dlc,index) in dlcList" :key="index">
                        <div class="m-hot clearbox">
                            <div class="m-hot-left fl">
                                <a href="#"><img :src=dlc.dlcImg alt=""  width="161" height="128"></a>
                            </div>
                            <div class="m-hot-right fr">
                                <div class="dlc-content">
                                    <h4>{{ dlc.dlcName }}</h4>
                                    <p class="dlc-intro">{{ dlc.dlcIntro }}</p>
                                </div>
                                <div>
                                    <div class="dlc-publish-time">发布于 {{ dlc.dlcPublishDate }}</div>
                                    <a class="dlc-add" href="#">加入购物车 ￥{{ dlc.dlcPrice }}</a>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>

            </div>
            <!--			应用特性-->
            <div class="m-content-right fr">
                <div class="panel">
                    <h2>应用特性</h2>
                    <table>
                        <tr >
                            <td ><span class="iconfont icon-gouwucheman"></span></td>
                            <td >含内购项</td>
                        </tr>
                        <tr>
                            <td ><span class="iconfont icon-duoren"></span></td>
                            <td>支持多人联机</td>
                        </tr>
                        <tr>
                            <td ><span class="iconfont icon-youxiguanli"></span></td>
                            <td >支持手柄</td>
                        </tr>
                        <tr>
                            <td ><span class="iconfont icon-bi"></span></td>
                            <td >开发者汉化</td>
                        </tr>
                        <tr >
                            <td ><span class="iconfont icon-lianjiezhuangtai"></span></td>
                            <td >不支持离线模式</td>
                        </tr>
                    </table>
                </div>
                <!--				配置需求-->
                <div class="panel">
                    <h2>最低配置要求</h2>
                    <table >
                        <tr >
                            <td >内存</td>
                            <td>4 GB</td>
                        </tr>
                        <tr>
                            <td>显卡</td>
                            <td>英伟达® GeForce GTX 650</td>
                        </tr>
                        <tr >
                            <td >硬盘</td>
                            <td >15 GB</td>
                        </tr>
                        <tr >
                            <td >CPU</td>
                            <td >英特尔® 酷睿i5-3330</td>
                        </tr>
                        <tr >
                            <td>系统</td>
                            <td>Windows 7(64位)</td>
                        </tr>
                    </table>
                </div>
                <!--				语言-->
                <div class="panel">
                    <h2>语言</h2>
                    <table>
                        <tr>
                            <td>界面</td>
                            <td >中文简体 </td>
                        </tr>
                        <tr>
                            <td >音频</td>
                            <td>中文简体 </td>
                        </tr>
                        <tr >
                            <td>文本</td>
                            <td >中文简体 </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "GameInfo",
    data() {
        return {
            introDetail:{
                introTitle:'艾尔登法环是以正统黑暗奇幻世界为舞台的动作RPG游戏 ',
                introSum:'  走进辽阔的场景与地下迷宫探索未知，挑战困难重重的险境，享受克服困境时的成就感吧。 不仅如此，登场角色之间的利害关系谱成的群像剧，更是不容错过。',
                detailList:[
                    {
                        index:0,
                        content:{
                            title:'・充满刺激的辽阔世界',
                            content: '无缝连接的辽阔世界──尽情探索状况多变的开放场景，构造复杂、立体的巨大地下迷宫。 在探索新发现时感到欣喜，在征服强敌时感到成就感十足，丰富内容等你体会。',
                        }
                    },
                    {
                        index:1,
                        content:{
                            title:'・专属于你的角色',
                            content: ' 不只能自订操纵角色的外表，更能自行搭配武器、装备，与魔法──想成为力量取胜的顽强战士，或选择在魔法领域登峰造极，都能照自己的游玩风格决。',
                        },

                    },
                    {
                        index:2,
                        content:{
                            title:'・源自神话的群像剧',
                            content: '片段破碎，却又多彩的故事──以“交界地”为舞台登场的角色们各有主张， 于是在利害关系之下谱成的群像剧，就此展开。',
                        },
                    },
                    {
                        index:3,
                        content:{
                            title:'・与其他玩家维持非即时连系的独特系统',
                            content: '除了能与其他玩家一同游玩的多人联机游玩以外， 本游戏也有非即时联机系统，能借此得知其他玩家的存在。',
                        },
                    },
                ],
            },
            dlcList:[
                {
                    index:0,
                    dlcName:'DARK SOULS™ III - Ashes of Ariandel™',
                    // dlcImg:require('../assets/imgs/bs3-dlc1.jpg'),
                    dlcIntro:'"你就是无火的余灰。\n' +
                        '                        Dark Souls™ III Season Pass内容之一。取得DLC包“艾雷德尔之烬，”拓展您的游戏体验。\n' +
                        '                        踏上探索雪白世界──艾雷德尔的旅程，挑战新地图、头目、敌人，以及取得新武器、防具套装与法术。\n' +
                        '                        您愿意接受挑战，再次拥抱黑暗吗？"',
                    dlcPublishDate:'2016年10月25日',
                    dlcPrice:'78.00',
                },
                {
                    index:1,
                    dlcName:'DARK SOULS™ III - The Ringed City™',
                    // dlcImg:require('../assets/imgs/bs3-dlc2.jpg'),
                    dlcIntro:'您毋须畏惧黑暗，灰烬大人。\n' +
                        '                        赢得具指标性的Golden Joystick Awards 2016 Game of the year RPG──DARK SOULS III，即将迎来最后一个DLC包。\n' +
                        '                        DARK SOULS III的最后一段旅程──环印城，前往探索末世所在，发掘新区域、新头目、新装备、法术，以及道具。\n' +
                        '                        是新世界，也是最终章──尽情体验仅有宫崎英高先生才能创造出的史诗最终篇章。',
                    dlcPublishDate:'2017年3月28日',
                    dlcPrice:'78.00',
                },
            ]
        }
    },
    methods:{
        click2Show:function (){

            if(this.$refs["m-introduction"].style.overflow ==='visible')
            {
               this.$refs["m-introduction"].style.height ='305px';
                this.$refs["m-introduction"].style.overflow ='hidden';
                this.$refs.icons.className = "iconfont icon-jiantou9";
            }
            else{
                this.$refs["m-introduction"].style.height ='800px';
                this.$refs["m-introduction"].style.overflow ='visible';
                this.$refs.icons.className = 'iconfont icon-iconfonticontrianglecopy';
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

.g-bd{
    width: 1005px;
    height: 1700px;
    margin: 0 auto;
    margin-top: 20px;
}
.m-content{
    width: 1005px;
    margin: 0 auto;
    height: 1200px;
    margin-top: 20px;
}

/*游戏详情*/
.m-content-left{
    width: 705px;
    height: 1200px;

}
.game-poster{
    width: 98%;
    height: auto;
    margin: auto;
}
.m-tt{
    font-weight: normal;
    margin-bottom: 10px;
    margin-top: 20px;
}
.m-introduction{
    width:703px;
    height:305px;
    background-color: #fff;
    overflow: hidden;
    position: relative;
}
.intro-title{
    margin-left: 10px;
    margin-top: 20px;
    font-size: 14px;
    text-align: left;
}
.intro-sum{
    margin-top: 10px;
    margin-left: 10px;
    margin-right: 10px;
    font-size: 12px;
    text-align: left;
}

.u-hide{
    display: block;
    position: absolute;
    text-align: center;
    font-size: 16px;
    width: 700px;
    height: 100px;
    line-height: 180px;
    bottom:0px;
    background-image: linear-gradient(to top, white , transparent);
    z-index: 99;
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
    height: 1200px;
    width: 285px;
}
.panel{
    width: 285px;
    height: 272px;
    background-color: #fff;
    margin-bottom: 20px;
}
.panel h2{
    padding-top: 20px;
    padding-left: 20px;
    font-weight: normal;
}

.panel table{
    margin-top: 20px;
    margin-left: 20px;
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
.panel table tr td:last-of-type{
    padding-left: 10px;
}
.panel:nth-of-type(3){
    height:230px;
}
.panel:nth-of-type(3) table tr td:first-of-type{
    color: gray;
}
.panel:last-of-type{
    height: 200px;
}

.panel:last-of-type p{
    margin-left: 20px;
    margin-top: 20px;
    color: gray;
}
.panel:last-of-type a{
    margin-left: 20px;
    display: block;
    margin-top: 5px;
    color: gray;
}
.panel:last-of-type p:last-of-type{
    margin-top: 5px;
}

.m-content-left .u-tips{
    margin-top: 20px;
    border: 1px solid gray;
    display: block;
    width: 70px;
    text-align: center;
    height: 30px;
    line-height: 30px;

}
.m-hot{
    width: 703px;
    height: 148px;
    background-color: #fff;
    margin-bottom: 20px;

    border: #000000 1px solid;
}
.m-hot-left{
    width: 195px;
    height: 148px;
}
.m-hot-left a{
    display: block;
    width:161px;
    height:107px;
    margin-left: 10px;
    margin-top: 10px;
}
.m-hot-right{
    height: 148px;
    width: 505px;
}
.m-hot-right h4{
    padding-top: 10px;
    font-size: 14px;
    float: left;
}
.m-hot-right span{
    float: left;
    margin-top: 25px;
    color: gray;
}
.m-hot-right p:first-of-type{
    margin-top: 10px;
    width: 480px;
    font-size: 12px;
    float: left;
    text-align: left;
}

.m-hot-right p:nth-of-type(2){
    height: 20px;
    margin-left: 10px;
    margin-top: 10px;
    float: left;
}
.dlc-content{
    height: 110px;
    border: #000000 1px solid;
}
.dlc-publish-time{
    height: 20px;
    width: 200px;
    margin-left: 10px;
    display: block;
    margin-top: 10px;
    float: left;
    border: #000000 1px solid;
}
.dlc-add{
    border: #000000 1px solid;
    width: 200px;
    height: 20px;
    float: right;
    margin-top: 10px;
    margin-right: 10px;
    color: white;
    font-size: 14px;
    text-align: center;
    font-weight: 600;
    background-color: #3B9CFF;
}
</style>
