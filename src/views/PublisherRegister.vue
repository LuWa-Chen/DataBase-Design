<template>
	<div class="login-register">
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
		<div class="contain">
			<div>

				<div class="big-contain" key="bigContainRegister" :class="{active:isLogin}">
                    <br><br><br><br><br><br><br><br><br><br>
				    <div class="btitle">创建发行商账户</div>
				        <br><br>
				        <div class="bform">
				    	    <input type="text" placeholder="发行商用户名" v-model="form.username">
				    	    <span class="errTips" v-if="name_existed">* 用户名已经存在！ *</span>
                            <br><br><br>
				    	    <input type="email" placeholder="邮箱" v-model="form.useremail">
				    	    <span class="errTips" v-if="email_existed">* 邮箱已经存在！ *</span>
                            <br><br><br>
				    	    <input type="password" placeholder="密码" v-model="form.userpwd">
                            <br><br><br>
                            <input  placeholder="电话联系方式" v-model="form.phonenum">
                            <br><br>
				    	</div>
				    	<button class="bbutton" @click="register">注册</button>
				    </div>
			    </div>

                <div class="small-box" :class="{active:isLogin}">
                	<div class="small-contain" key="smallContainRegister" v-if="isLogin">
                	    <div class="stitle">发行商账户登录</div>
                        <div class="bform">
                        <input type="email" placeholder="邮箱" v-model="form.useremail">
                        <br>
                        <span class="errTips" v-if="emailError">* 邮箱填写错误 *</span>
                        <input type="password" placeholder="密码" v-model="form.userpwd">
                        <span class="errTips" v-if="emailError">* 密码填写错误 *</span>
                        </div>
                        <button class="sbutton" @click="login">登录</button>
                        <br><br><br>
                		<button class="sbutton" @click="changeType">立即注册</button>
                	</div>
                	<div class="small-contain" key="smallContainLogin" v-else>
                		<button class="sbutton" @click="changeType">已有发行商账户？</button>
                	</div>
                </div>
		</div>
	</div>
</template>

<script>
import GamePageHeader from "@/views/GamePageHeader";
export default{
	name:'login-register',
	components: {
      GamePageHeader,
    },
	data(){
		return {
            action: 'https://jsonplaceholder.typicode.com/posts/',
            mode: {},
            modeList: [],
		    isLogin:false,
		    emailError: false,
		    passwordError: false,
		    name_existed: false,
		    email_existed: false,
		    form:{
		    	username:'',
		    	useremail:'',
		    	userpwd:'',
                intro:'',
                phonenum:'',
                isUser:'Publisher',

		    }
		}
	},
	methods:{



		changeType() {
			this.isLogin = !this.isLogin
			this.form.username = ''
			this.form.useremail = ''
			this.form.userpwd = ''
			this.form.phonenum = ''
		},
		login() {
            const self = this;
            if (self.form.useremail != "" && self.form.userpwd != "") {
            	self.$axios({
            		method:'post',
            		url: 'api/publisher/login',
            		data: {
            			email: self.form.useremail,
            			password: self.form.userpwd
            		}
            	})
            	.then( res => {
            		switch(res.data.result){
            			case 0:
            			    console.log(res);
            				alert("发行商账户登录成功！");
                            this.$store.commit('changeValue_Name',res.data.name);
                            this.$store.commit('changeValue_ID',res.data.id);
                            this.$store.commit('change_flag',this.form.isUser);
                            this.$router.push({name:'GamePage'});
            				break;
            			case -1:
            				this.emailError = true;
            				break;
            			case 1:
            				this.passwordError = true;
            				break;
            		    case -2:
                            alert("网络连接失败")
                            break;
            		}
            	})
            	.catch( err => {
            		console.log(err);
            	})
            } else{
            	alert("填写不能为空！");
            }
        },
		register()
        {
            if(this.isLogin)
                return;
			const self = this;
			let year = new Date().getFullYear();
			let month = new Date().getMonth() +1;
			let day = new Date().getDate();
			if(self.form.username != "" && self.form.useremail != "" && self.form.userpwd != "" && self.form.phonenum != ""){
				self.$axios({
					method:'post',
					url: 'api/publisher/register',
					data: {
						username: self.form.username,
						email: self.form.useremail,
						password: self.form.userpwd,
						phonenum: self.form.phonenum,
						time:year + '-' + month + '-' + day,
						area:'中国'
					}
				})
				.then( res => {
					switch(res.data.result){
						case 0:
							alert("注册成功！");
							this.login();
							break;
						case -1:
							this.name_existed = true;
							break;
						case 1:
						    this.email_existed = true;
						    break;
						case -2:
						    alert("网络连接失败")
						    break;
					}
				})
				.catch( err => {
					console.log(err);
				})
			} else {
				alert("填写不能为空！");
			}
        }
    }
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
	height: 70%;
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
	height: 10%;
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