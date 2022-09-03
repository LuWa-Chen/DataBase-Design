<!--1952168 张宇-->
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
        <div style="position:relative;width: 1055px;margin:100px auto;border-radius: 10px;background-color: #e0e0e0;overflow: hidden" class="clearbox">
          
	<div class="clearbox" style="display: flex">
                <el-dialog width="350px"  :visible="dialogVisible" :modal-append-to-body='false' @close="click2Close()">
                    <div slot="title" class="header-title">
                        <span> 臻Game 游戏购买 </span>
                    </div>
                    <p style="width: 220px;margin: auto;text-align: left">请选择支付方式：</p ><br/>
                    <div class="pay-button clearbox">
                        <span id="ali-pay" class="fl" @click="click2ChoosePay(1)">支付宝支付</span>
                        <span id="wechat-pay" class="fr"  @click="click2ChoosePay(2)">微信支付</span>
                    </div>
                    <div class="pay-amount clearbox">
                        <img class="fl" :src="payIco.ico" height="18px">
                        <span>&nbsp;{{payIco.name}}扫码，支付&nbsp;<span style="font-size: 18px;font-weight: bolder">{{Math.abs((sum-sub).toFixed(2))}}</span>&nbsp;元</span>
                    </div>
                    <span class="pay-done" v-if="this.sec<3">支付完成</span>
                    <div class="qr-code clearbox" id="qrcode" v-else> </div>
                </el-dialog>
            </div>
            <div class="shopping-cart">
                <div class="cart-head">
                    您已选择{{num}}款内容
                </div>
                <div class="cart-body">
                    <div style="height:  10px"></div>
                    <div class="no-game" v-if="gameList.length===0">
                        <img src="../assets/imgs/empty-cart.jpg">
                        <p>这里什么也没有~</p>
                    </div>
                    <ul>
                        <li v-for="(game,index) in gameList" :key="index"  class="cart-content clearbox">
                            <div class="fl" style="width: 40px;">
                                <input :id="getID(index,'checkbox-')" type="checkbox" @click="click2Choose(index)" style="height: 100px;margin-left: 10px">
                            </div>
                            <div class="m-game fl">
                                <div class="fl">

		<router-link :to="{name:'GameDetail',query:{game_id:game.id}}">
                                    <img :src="require('../../../ExGame-Asset/' + game.poster)" style="border-radius:5px">
		</router-link>
                                </div>
                                <div class="game-name fl" v-if="game.isDLC===false">
                                    <span >{{game.name}}</span>
                                </div>
                                <div class="game-name-dlc fl" v-if="game.isDLC===true">
                                    <span >{{game.name}}</span>
                                    <p>此DLC依赖本体才可运行，请先购买本体</p>
                                    <!--                                <a>前往购买</a>-->
                                </div>
                                <span class="game-price fr" v-if="game.discount!==1">￥{{game.price}}</span>
                                <span class="game-discount fr">￥{{game.price*game.discount}}</span>
                            </div>
                            <span class="delete-button fr" @click="click2Delete(index)">移除</span>
                        </li>
                    </ul>
                    <div style="height:  5px"></div>
                </div>
                <div class="cart-foot clearbox">
                    <div class="m-user clearbox">
                        <span class="fl" style="font-size: 12px;color: #666666">购买至：</span>
                        <div @click="click2OpenFriend()">
                            <div class="user-head fl">
                               
		<img  :src="require('../../../ExGame-Asset/User/' + user.id +'/ProfilePhoto.jpg')" height="36">
                            </div>
                            <div class="user-info fl">
                                <div style="font-weight: bolder;display: inline-block;">{{user.name}}</div>
                                <div class="fr" style="color: #666666;display: inline;">（id：{{user.id}}）</div>
                            </div>
                        </div>
                        <div class="fr" style="margin-right: 20px">
                            {{num}}&nbsp;款内容合计：￥{{Math.abs(sum.toFixed(2))}}
                        </div>
                    </div>
                    <div class="buy-info clearbox">
                        <div class="fl">
                           活动优惠￥ {{ sub.toFixed(2) }}
                        </div>
                        <div class="fr" style="width: 350px">
                        <span class="fl" style="font-size: 14px">
                           应付总金额：<span style="font-size: 20px;color: orange">￥{{Math.abs((sum-sub).toFixed(2))}}</span>
                        </span>
                            <span class="buy-button fr" @click="click2Pay()">确认购买</span>
                        </div>
                    </div>
                </div>
            </div>
            <div style="height: 20px"></div>
        </div>
        <div id="s1" class="select-bar-s clearbox" style="z-index: 999;display: none;position: relative">
            <div style="text-align: center;line-height: 50px" v-if="friendList.length===1">
                没有好友
            </div>
            <ul v-else>
                <li :id="getID(index+1,'s1l')" v-for="(friend,index) in friendList" :key="index" @click="click2ChooseFriend(index)" @mouseenter="onHover(getID(index+1,'s1l'))" @mouseleave="onLeave(getID(index+1,'s1l'))">
                    <div class="friend-head fl">
                        <img  :src="require('../../../ExGame-Asset/User/' + friend.id +'/ProfilePhoto.jpg')"  alt="" height="36" width="36" >
                   
                    </div>
                    <div class="friend-info fl">
                        <div class="clearbox" style="width: 350px">
                            <div style="font-weight: bolder;display: inline-block;">{{friend.name}}</div>
                            <div class="fr" style="color: #666666;display: inline;">（id：{{friend.id}}）</div>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <GamePageFooter/>
    </div>
</template>

<script>
import QRCode from "qrcodejs2";
import GamePageHeader from "@/views/GamePageHeader";
import GamePageFooter from "@/components/GamePageFooter";

export default {
    name: "ShoppingCart",
    data() {
        return {
            sum:0,
            num:0,
            sub:0,
            dialogVisible:false,
            sec:10,
            user:{
                head:require('../../../ExGame-Asset/' + this.$store.state.isUser +'/' + this.$store.state.userID + '/' + this.$store.state.logo_type),
                name:this.$store.state.userName,
                id:this.$store.state.userID,
            },
            payIco: {
                name:"支付宝",
                ico:require('../assets/imgs/ali-pay.jpg')
            },
            payPattern:[
                {
                    name:"支付宝",
                    ico:require('../assets/imgs/ali-pay.jpg')
                },
                {
                    name:"微信",
                    ico:require('../assets/imgs/wechat-pay.jpg')
                }
            ],
            chosenIndex:[],
            gameList:[],
            friendList:[],
            ownedGame:[],
             user_is :'',
        }
    },
    components: {
            GamePageHeader,
            GamePageFooter
        },
    mounted() {
        // console.log('+++++++++++++' + this.user_id)
        this.user_is = this.$store.state.userID
        this.getData(this.user_is)
        this.getUserLib(this.user_is)
        this.getFriend(this.user_is)
    },
methods:{
        getID:function (index,pre){
            return pre + index.toString()
        }, getUserLib(uid){
            const self = this;
            self.$axios({
                method:'post',
                url: 'api/user/getUserGameInfo',
                data: {
                    id:uid,
                }
            }).then( res => {
                if(res.data.result===1) {
                    console.log("数据库连接正常！")
                    console.log(self.num);
                    self.ownedGame = []
                    for(let i=0;i< res.data.game_num;i++)
                    {
                        console.log('用户已经有' + res.data.game_info_set[i].game_name)
                        self.ownedGame.push(res.data.game_info_set[i].game_id)
                    }
                }
                else{
                    alert("数据库链接出错，请重试！");
                }
            }).catch( err => {
                console.log(err);
            })
        },
        checkUserLib(gid){
          for(let i = 0;i<this.ownedGame.length;i++)
          {
              if(gid===this.ownedGame[i])
              {
                  alert('已拥有该游戏')
                  return false
              }
          }
        },
        getData:function (uid){
            if(uid.length===0)
            {
                alert('uid 不能为空')
                return;
            }
            this.$axios.post('api/shopingcart/getUserShoppingCart', {
                user_id: uid,
            }).then( res => {
                switch(res.data.result){
                    case 1:
                        console.log("购物车 请求成功");
                        break;
                    default:
                        console.log('购物车 请求失败')
                        break;
                }
                for(let i in res.data.game_list)
                {
                    console.log(res.data.game_list[i].id)
                    console.log('折扣' + res.data.game_list[i].discount.toString())
                    console.log(res.data.game_list[i].poster)
                    this.gameList.push({
                        id:res.data.game_list[i].id,
                        poster:res.data.game_list[i].poster,
                        name:res.data.game_list[i].name,
                        price:res.data.game_list[i].price,
                        discount:res.data.game_list[i].discount,
                        isDLC:res.data.game_list[i].is_dlc,
                    })
                    this.chosenIndex.push(false)
                }
            }).catch( err => {
                console.log(err);
            })
        },
        deleteData:function (gid,op){
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
      click2Choose:function (index){
            if(this.checkUserLib(this.gameList[index].id)===false)
            {
                document.getElementById(this.getID(index,'checkbox-')).checked = false
                return
            }
            var box = document.getElementById(this.getID(index,'checkbox-'))
            if(this.chosenIndex[index] === false)
            {
                console.log(index + '选中')
                console.log( this.sum.toString() +'s+ ' + this.gameList[index].price.toString())
                console.log( this.sub.toString() +'s- ' + this.gameList[index].price.toString() + " dis  " + (1-this.gameList[index].discount).toString())
                this.chosenIndex[index] = true;
                this.num++;
                this.sum+=this.gameList[index].price;
                this.sub+=this.gameList[index].price*(1-this.gameList[index].discount);

                box.checked  = true;
            }
            else
            {
                console.log(index + '取消')
                console.log( this.sum.toString() +'s+ ' + this.gameList[index].price.toString())
                console.log( this.sub.toString() +'s- ' + this.gameList[index].price.toString() + "   " + (1-this.gameList[index].discount).toString())
                this.chosenIndex[index] = false;
                this.num--;
                this.sum-=this.gameList[index].price;
                this.sub-=this.gameList[index].price*(1-this.gameList[index].discount);
                box.checked  = false;
            }
        },
        click2Delete:function (index){
            console.log(index)
            this.deleteData(this.gameList[index].id,2)
            if(this.chosenIndex[index] === true)
            {
                this.chosenIndex[index] = false;
                this.num--;
                this.sum-=this.gameList[index].price;
                this.sub-=this.gameList[index].price*(1-this.gameList[index].discount);
            }
            // var box = document.getElementById(this.getID(index,'checkbox-'))
            // box.checked  = false;
            var tempList = [], temp = []
            for(let i=0;i<this.gameList.length;i++)
            {
                if(i!==index)
                {
                    tempList.push(this.gameList[i])
                    temp.push(this.chosenIndex[i])
                }
            }
            this.gameList = tempList;
            this.chosenIndex = temp;
            for(let i=0;i<this.gameList.length;i++)
            {
                // console.log(this.chosenIndex[i])
                if(this.chosenIndex[i]===true)
                {
                   document.getElementById(this.getID(i,'checkbox-')).checked = true;
                }
                else
                {
                    document.getElementById(this.getID(i,'checkbox-')).checked = false;
                }
            }
        },
        click2Pay:function (){
            if(this.gameList.length===0)
            {
                alert('当前购物车没有游戏')
                return
            }
            if(this.num>0)
            {
                this.dialogVisible = true;
                this.getSencond()
                setTimeout(this.click2ChoosePay,100,1)
            }
            else
            {
                alert('请选择一款游戏')
            }
        },
        click2Close:function (){
            this.dialogVisible = false;
            this.sec = 10;
            for(let i=0;i<this.gameList.length;i++)
            {
                if(this.chosenIndex[i]===true)
                {
                    console.log(this.gameList[i].id)
                    this.add2Library(this.$store.state.userID,this.gameList[i].id)
                    this.createOrder(this.$store.state.userID,this.gameList[i].id,parseFloat((this.sum - this.sub).toFixed(2)),this.user.id)
                }
            }
            for(let i=0;i<this.gameList.length;i++)
            {
                if(this.chosenIndex[i]===true)
                {
                    console.log('success' + this.gameList[i].id)
                    this.click2Delete(i)
                }
            }
        },
        click2ChoosePay:function (type){
            if(type===1)
            {
                this.createQRcode ('ali' + this.sum)
                this.payIco = this.payPattern[0];
                document.getElementById('ali-pay').style.border = '#3B9CFF 1px solid'
                document.getElementById('ali-pay').style.backgroundColor = 'rgb(187, 255, 255, 20%)'
                document.getElementById('wechat-pay').style.border = '#eeeeee  1px solid'
                document.getElementById('wechat-pay').style.backgroundColor = 'transparent'
            }
            else {
                this.createQRcode ('wechat' + this.sum)
                this.payIco = this.payPattern[1];
                document.getElementById('wechat-pay').style.border = '#00EE76 1px solid'
                document.getElementById('wechat-pay').style.backgroundColor = 'rgb(127, 255, 212, 10%)'
                document.getElementById('ali-pay').style.border = '#eeeeee  1px solid'
                document.getElementById('ali-pay').style.backgroundColor = 'transparent'
            }
            this.sec=10;

        },
        createQRcode (url) {
            var qrcode = document.getElementById("qrcode");
            qrcode.innerHTML = '';
            new QRCode(qrcode, {
                text: url,
                width: 200,
                height: 200,
                colorDark : "#000000",
                colorLight : "#ffffff",
                correctLevel : QRCode.CorrectLevel.H,
            });
        },
        getSencond :function() {
            const that = this
            const interval = window.setInterval(function () {
                --that.sec
                if (that.sec === 0) {
                    that.isDisabled = false
                    window.clearInterval(interval)
                    that.dialogVisible = false;
                }
            }, 1000)
        },
        click2OpenFriend:function (){
            var id = 's1'
            var ss  = document.getElementById(id)
            if(ss.style.display === 'block')
            {
                ss.style.display = 'none'
            }
            else
            {
                ss.style.display = 'block';
            }
        },
        click2ChooseFriend:function (index){
            this.user = this.friendList[index]
            this.getUserLib(this.user.id)
            var ss  = document.getElementById('s1')
            if(ss.style.display === 'block')
            {
                ss.style.display = 'none'
            }
            else
            {
                ss.style.display = 'block';
            }
        },
        onHover:function (id){
            var it =  document.getElementById(id)
            it.style.backgroundColor = '#e9e9e9'

        },
        onLeave:function (id){
            var it =  document.getElementById(id)
            it.style.backgroundColor = 'transparent';
        },
        getFriend:function (uid){
            if(uid.length===0)
            {
                alert('uid 不能为空')
                return;
            }
            this.$axios.post('api/user/getFriendsList', {
                id: uid,
            }).then( res => {
                switch(res.data.result){
                    case 1:
                        console.log("好友 请求成功");
                        break;
                    default:
                        console.log('好友 请求失败')
                        break;
                }
                this.friendList.push(this.user)
                for(let i in res.data.friends_list)
                {
                    console.log(res.data.friends_list[i].id + res.data.friends_list[i].profile_photo + '++++++')
                    this.friendList.push(
                        {
                            head:'h1.jpg',
                            name:res.data.friends_list[i].name,
                            id:res.data.friends_list[i].id,
                        }
                    )
                }
            }).catch( err => {
                console.log(err);
            })
        },
        add2Library:function (uid,gid){
            if(uid.length===0||gid.length===0)
            {
                alert('uid 不能为空')
                return;
            }
            this.$axios.post('api/shopingcart/addToLibrary', {
                user_id: uid,
                game_id: gid,
            }).then( res => {
                switch(res.data.result){
                    case 1:
                        console.log("入库 请求成功");
                        break;
                    case -1:
                        console.log('连接失败')
                        break;
                    default:
                        console.log('入库 请求失败')
                        break;
                }
            }).catch( err => {
                console.log(err);
            })
        },
        createOrder:function (uid,gid,pam,rid){
            if(gid.length!==10)
            {
                console.log('gid 必须为10')
            }
            if(uid.length!==10)
            {
                console.log('uid 必须为10')
            }
            if(pam<0)
            {
                console.log('付款金额不小于0')
            }
            console.log(pam)
            this.$axios.post('api/shopingcart/postOrders', {
                user_id: uid,
                game_id: gid,
                via_cdk:1,
                pay_amount:pam,
                recept_id:rid
            }).then( res => {
                switch(res.data.result){
                    case 1:
                        console.log("订单 请求成功");
                        break;
                    case -1:
                        console.log('连接失败')
                        break;
                    default:
                        console.log('订单 请求失败')
                        break;
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
  height: 174%;
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
    width:100%;
    background-color: black;
    overflow: hidden;
    color: black;
}
.shopping-cart{
    width: 1005px;
    margin:  auto;
    /*border: black 2px solid;*/
}
.cart-head{
    height: 60px;
    font-weight: bolder;
    font-size: 22px;
    line-height: 60px;
}
.cart-body{
    background-color: white;
    min-height: 300px;
    border-radius: 10px;
}
.no-game{
    font-size: 16px;
    text-align: center;
}
.no-game p{
    margin-top: 10px;
    font-weight: bold;
    color: #aaaaaa;
    font-family: Arial, YouYuan, sans-serif;
}
.cart-content{
    border: #795da3 1px solid;
    height: 100px;
    margin: 0 10px 10px 10px;
    border-radius: 10px;
}
.m-game{
    height: 100PX;
    width: 800px;
}
.m-game img{
    height: 80PX;
    margin-top: 10px;
}
.game-name{
    margin-left: 20px;
    height: 100PX;
    line-height: 100px;
    font-size: 22px;
    font-weight: bolder;
}
.game-name-dlc{
    margin-left: 20px;
    height: 100PX;
    font-size: 22px;
    font-weight: bolder;
}
.game-name-dlc span{
    height: 60px;
    line-height: 60px;
    display: inline-block;
}
.game-name-dlc p{
    font-size: 13px;
    color: red;
    font-weight: normal;
}
.game-name-dlc a{
    font-size: 13px;
    color: orange;
    font-weight: normal;
    text-decoration: underline;
}
.game-price{
    margin-left: 20px;
    height: 100PX;
    line-height: 100px;
    font-size: 16px;
    text-decoration: line-through;
    color: #aaaaaa;
}
.game-discount
{
    margin-left: 20px;
    height: 100PX;
    line-height: 100px;
    font-size: 18px;
    font-weight: bolder;
    color: orange;
}
.delete-button{
    height: 100PX;
    line-height: 100px;
    margin-right: 50px;
    font-size: 13px;
    text-decoration: underline;
}
.delete-button:hover{
    color: #3B9CFF;
}
.cart-foot{
    margin-top: 10px;
    background-color: white;
    float: bottom;
    height: 120px;
    border-radius: 10px;
}
.m-user{
    height: 60px;
    line-height: 60px;
    border-bottom: #aaaaaa 1px solid;
    border-bottom-style: dashed;
    width: 945px;
    margin: auto;
}
.user-head{
    border:  white 2px solid;
    width: 40px;
    height: 40px;
    margin-top: 10px;
    border-radius:50%;
    overflow:hidden;
}
.user-info{
    margin-left: 10px;
    width: 350px;
}
.friend-head{
    border:  white 2px solid;
    width: 40px;
    height: 40px;
    margin-left: 7px;
    border-radius:50%;
    overflow:hidden;
}
.friend-info{
    margin-left: 10px;
    line-height: 40px;
    /*width: 200px;*/
}
.select-bar-s{
    border: #aaaaaa 1px solid;
    top:-190px;
    right: -420px;
    width: 430px;
    height: 180px;
    border-radius: 8px;
    align-items: center;
    background-color: white;
    overflow-y: auto;
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
.select-bar li{
    height: 40px;
    /*font-size: 12px;*/
    line-height:40px;
    margin-top: 5px;
}

.buy-info{
    height: 60px;
    line-height: 60px;
    width: 945px;
    margin: auto;
}
.buy-button{
    font-weight: bolder;
    font-size: 16px;
    width: 140px;
    height: 36px;
    text-align: center;
    border-radius: 18px;
    line-height: 36px;
    color: white;
    background-color: #3B9CFF;
    margin-top: 12px;
    display: inline-block;
    -webkit-user-select:none ;
    -moz-user-select:none;
}



.header-title{
    text-align: center;
    height: 0;
}
.pay-button{
    width: 220px;
    height: 50px;
    margin: auto;
    color: black;
}
#ali-pay{
    width: 100px;
    height: 40px;
    line-height: 40px;
    text-align: center;
    border: #eeeeee 1px solid;
    display: inline-block;
    border-radius: 5px;
    -webkit-user-select:none ;
    -moz-user-select:none;
}
#wechat-pay{
    width: 100px;
    line-height: 40px;
    text-align: center;
    height: 40px;
    border: #eeeeee 1px solid;
    display: inline-block;
    border-radius: 5px;
    -webkit-user-select:none ;
    -moz-user-select:none;
}
.pay-amount{
    height: 20px;
    margin: 10px auto;
    display: flex;
    justify-content: center;
    align-items: center;
    line-height: 20px;
    color: black;
}

.qr-code{
    width: 202px;
    height: 202px;
    border: white 1px solid;
    margin:  auto;
}
.qr-code img{
    margin-left: 55px;
    border: #e7e1cd 1px solid;
}
.pay-done{
    display: inline-block;
    height: 30px;
    margin-top: 20px;
    margin-left: 125px;
    text-align: center;
    line-height: 30px;
    /*border: red 1px solid;*/
}


</style>
