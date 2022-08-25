<template>
    <div class="canvas">
        <div class="clearbox" style="display: flex">
            <el-dialog width="350px"  :visible="dialogVisible" :modal-append-to-body='false' @close="dialogVisible = false">
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
                    <span>&nbsp;{{payIco.name}}扫码，支付&nbsp;<span style="font-size: 18px;font-weight: bolder">{{sum-sub}}</span>&nbsp;元</span>
                </div>
                <div class="qr-code clearbox" id="qrcode"> </div>
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
                               <img :src=game.poster>
                           </div>
                           <div class="game-name fl" v-if="game.isDLC===false">
                               <span >{{game.name}}</span>
                           </div>
                           <div class="game-name-dlc fl" v-if="game.isDLC===true">
                               <span >{{game.name}}</span>
                               <p>此DLC依赖本体才可运行，请先购买本体</p>
                               <!--                                <a>前往购买</a>-->
                           </div>
                           <span class="game-price fr" v-if="game.discount!==100">￥{{game.price}}</span>
                           <span class="game-discount fr">￥{{game.price * game.discount/100}}</span>
                       </div>
                       <span class="delete-button fr" @click="click2Delete(index)">移除</span>
                   </li>
               </ul>
               <div style="height:  5px"></div>
           </div>
           <div class="cart-foot clearbox">
               <div class="m-user">
                   <span class="fl" style="font-size: 12px;color: #666666">购买至：</span>
                   <div class="user-head fl">
                       <img  :src="require('../assets/imgs/' + user.head)"  alt="" height="36" width="36" >
                   </div>
                   <div class="user-info fl">
                       <span style="font-weight: bolder">{{user.name}}</span>
                       <span style="color: #666666">（id：{{user_id}}）</span>
                   </div>
                   <div>
                       {{num}}&nbsp;款内容合计：￥{{sum}}
                   </div>
               </div>
               <div class="buy-info clearbox">
                   <div class="fl">
                       活动优惠￥ {{ sub }}
                   </div>
                   <div class="fr" style="width: 350px">
                        <span class="fl" style="font-size: 14px">
                            应付总金额：<span style="font-size: 20px;color: orange">￥{{sum-sub}}</span>
                        </span>
                       <span class="buy-button fr" @click="click2Pay()">确认购买</span>
                   </div>
               </div>
           </div>
       </div>
    </div>
</template>

<script>
import QRCode from "qrcodejs2";

export default {
    name: "ShoppingCart",
    props:['user_id'],
    data() {
        return {
            sum:0,
            num:0,
            sub:0,
            dialogVisible:false,
            user:{
                head:'h1.jpg',
                name:'123456',
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
            chosenIndex:[false,false,false],
            gameList:[
                {
                    id:'',
                    poster:require("../assets/imgs/season_pass.jpg"),
                    name:'ring',
                    price:123,
                    discount:80,
                    isDLC:false,
                },
                {
                    id:'',
                    poster:require("../assets/imgs/season_pass.jpg"),
                    name:'ring',
                    price:1,
                    discount:100,
                    isDLC:true,
                },
                {
                    id:'',
                    poster:require("../assets/imgs/season_pass.jpg"),
                    name:'ring',
                    price:12,
                    discount:100,
                    isDLC:false,
                },
            ],
        }
    },
    mounted() {
        console.log('+++++++++++++' + this.user_id)
    },
    methods:{
        getID:function (index,pre){
            return pre + index.toString()
        },
        click2Choose:function (index){
            console.log(index)
            var box = document.getElementById(this.getID(index,'checkbox-'))
            if(this.chosenIndex[index] === false)
            {
                this.chosenIndex[index] = true;
                this.num++;
                this.sum+=this.gameList[index].price;
                this.sub+=this.gameList[index].price*(100-this.gameList[index].discount)/100;
                box.checked  = true;
            }
            else
            {
                this.chosenIndex[index] = false;
                this.num--;
                this.sum-=this.gameList[index].price;
                this.sub-=this.gameList[index].price*(100-this.gameList[index].discount)/100;
                box.checked  = false;
            }
        },
        click2Delete:function (index){
            console.log(index)
            if(this.chosenIndex[index] === true)
            {
                this.chosenIndex[index] = false;
                this.num--;
                this.sum-=this.gameList[index].price;
                this.sub-=this.gameList[index].price*(100-this.gameList[index].discount)/100;
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
                setTimeout(this.click2ChoosePay,100,1)
            }
            else
            {
                alert('请选择一款游戏')
            }
        },
        click2Close:function (){
            this.$emit('click2Close')
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
    background-color: #e0e0e0;
    overflow: auto;
}
.shopping-cart{
    width: 1005px;
    min-height: 500px;
    margin: 100px auto;
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
    border-bottom-style: dotted;
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
    width: 670px;
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
</style>
