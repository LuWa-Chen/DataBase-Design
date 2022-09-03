<template>
	<div class="login-register">
	<game-page-header></game-page-header>
    <vue-particles
      color="#FF0000"
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
		<div class="contain">
                <div class="small-box">
                	<div class="small-contain" key="smallContainRegister"  @click="ToAdminPage" v-if="this.$store.state.pass_flag==true">
                	    <div class="stitle" >已验证身份，点击进入管理员界面</div>
                    </div>
                    <div class="small-contain" key="smallContainRegister" v-else>
                        <div class="stitle">未验证身份</div>
                    </div>
                </div>
		</div>
	</div>
</template>

<script>
import GamePageHeader from "@/views/GamePageHeader";
export default{
	name:'login-register',
	data:{
	  count:0,
	  return:{
	    isPass:false,
	  }
	},
	components: {
      GamePageHeader,
    },
    methods:{
        ToAdminPage(){
            this.$router.push({name:'AdminPage'});
        },

    },
    mounted(){
      var that = this;
      document.onkeydown = function(e){
        let key = window.event.keyCode;
        console.log(that.isPass)
        if(key == 87 && that.count == 0){
            that.count=1;
            console.log(that.count);
        }
        else if(key == 72 && that.count == 1){
            that.count=2;
            console.log(that.count);
        }
        else if(key == 73 && that.count == 2){
            that.count=3;
            console.log(that.count);
        }
        else if(key == 76 && that.count == 3){
            that.count=4;
            console.log(that.count);
        }
        else if(key == 69 && that.count == 4){
            that.count=5;
            console.log(that.count);
        }
        else if(key == 57 && event.shiftKey && that.count == 5){
            that.count=6;
            console.log(that.count);
        }
        else if(key == 84 && that.count == 6){
            that.count=7;
            console.log(that.count);
        }
        else if(key == 82 && that.count == 7){
            that.count=8;
            console.log(that.count);
        }
        else if(key == 85 && that.count == 8){
            that.count=9;
            console.log(that.count);
        }
        else if(key == 69 && that.count == 9){
            that.count=10;
            console.log(that.count);
        }
        else if(key == 48 && event.shiftKey && that.count == 10){
            that.count=11;
            console.log(that.count);
        }
        else if(key == 186 && that.count == 11){
            that.count=12;
            that.$store.state.pass_flag=true;
            return;
        }
        else if(key != 16){
            that.count=0;
            console.log(that.count);
        }
      }
    },
}
</script>

<style scoped="scoped">

@import "../assets/sea_css/index-style.css";
@import "../assets/sea_css/mixins.css";
@import "../assets/sea_css/cards.css";
@import "../assets/sea_css/variables.css";
#particles-js {
  width: 100%;
  height: calc(100% - 100px);
  position: absolute;
  z-index: 0;
}
.login-register{
	width: 100vw;
	height: 100vh;
	box-sizing: border-box;
	background: #121212;
}
.contain{
	width: 30%;
	height: 10%;
	position: relative;
	top: 45%;
	left: 50%;
	transform: translate(-50%,-50%);
	background-color: #fff;
	border-radius: 20px;
	box-shadow: 0 0 3px #f0f0f0,
				0 0 6px #f0f0f0;
}
.big-box{
	width: 100%;
	height: 100%;
	top: 0;
	left: 0;
	transform: translateY(0%);
	transition: all 1s;
}
.big-contain{
	top: 0;
	left: 0;
	width: 100%;
	height: 100%;
	display: flex;
	flex-direction: column;
	justify-content: center;
	align-items: center;
}
.btitle{
	font-size: 1.5em;
	font-weight: bold;
	color: #444444;
}
.bform{
	width: 100%;
	height: 40%;
	padding: 2em 0;
	display: flex;
	flex-direction: column;
	justify-content: space-around;
	align-items: center;
}
.bform .errTips{
	display: block;
	width: 50%;
	text-align: left;
	color: red;
	font-size: 0.7em;
	margin-left: 1em;
}
.bform input{
	width: 50%;
	height: 40px;
	border: none;
	outline: none;
	border-radius: 10px;
	padding-left: 2em;
	background-color: #f0f0f0;
}
.bbutton{
	width: 20%;
	height: 40px;
	border-radius: 24px;
	border: none;
	outline: none;
	background-color: #444444;
	color: #fff;
	font-size: 0.9em;
	cursor: pointer;
}
.bbutton:hover{
    background-color: #aaaaaa;
    color: #000000;
    transition: background-color 0.5s ease;
}
.small-box{
	width: 100%;
	height: 100%;
	background: linear-gradient(135deg,rgb(64,64,64),rgb(32,32,32));
	position: absolute;
	top: 0;
	left: 0;
	transform: translateY(0%);
	transition: all 1s;
	border-top-left-radius: inherit;
	border-bottom-left-radius: inherit;
}
.small-box.active{
    top: 0;
	height: 100%;
	border-top-left-radius: 0;
	border-bottom-left-radius: 0;
	border-top-right-radius: inherit;
	border-bottom-right-radius: inherit;
	transform: translateY(0);
	transition: height 0.6s;
}
.small-contain{
	width: 100%;
	height: 100%;
	display: flex;
	flex-direction: column;
	justify-content: center;
	align-items: center;
}
.small-contain span{
    color: #ffffff;
}
.stitle{
	font-size: 1.5em;
	font-weight: bold;
	color: #e0e0e0;
}
.scontent{
	font-size: 0.8em;
	color: #e0e0e0;
	text-align: center;
	padding: 2em 4em;
	line-height: 1.7em;
}
.sbutton{
	width: 60%;
	height: 40px;
	border-radius: 24px;
	border: 1px solid #fff;
	outline: none;
	background-color: transparent;
	color: #fff;
	font-size: 0.9em;
	cursor: pointer;
}
.sbutton:hover{
        background-color: #aaaaaa;
        color: #000000;
        transition: background-color 0.5s ease;
    }
.big-box.active{
	top: 10%;
	transform: translateY(-10%);
	transition: all 0.5s;
}

</style>