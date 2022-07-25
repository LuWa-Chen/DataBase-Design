<template>
	<div class="login-register">
		<div class="contain">
			<div class="big-box" :class="{active:isLogin}">
				<div class="big-contain" key="bigContainLogin" v-if="isLogin">
					<div class="btitle">账户登录</div>
					<div class="bform">
						<input type="email" placeholder="邮箱" v-model="form.useremail">
						<span class="errTips" v-if="emailError">* 邮箱填写错误 *</span>
						<input type="password" placeholder="密码" v-model="form.userpwd">
						<span class="errTips" v-if="emailError">* 密码填写错误 *</span>
					</div>
					<button class="bbutton" @click="login">登录</button>
				</div>
				<div class="big-contain" key="bigContainRegister" v-else>
					<div class="btitle">创建账户</div>
					<div class="bform">
						<input type="text" placeholder="用户名" v-model="form.username">
						<span class="errTips" v-if="existed">* 用户名已经存在！ *</span>
						<input type="email" placeholder="邮箱" v-model="form.useremail">
						<input type="password" placeholder="密码" v-model="form.userpwd">
					</div>
					<button class="bbutton" @click="register">注册</button>
				</div>
			</div>
			<div class="small-box" :class="{active:isLogin}">
				<div class="small-contain" key="smallContainRegister" v-if="isLogin">
					<div class="stitle">欢迎来到<span> 臻Game</span></div>
					<p class="scontent">开始注册，挑选你喜欢的游戏</p>
					<button class="sbutton" @click="changeType">注册</button>
				</div>
				<div class="small-contain" key="smallContainLogin" v-else>
					<div class="stitle">欢迎来到<span> 臻Game</span></div>
					<p class="scontent">登录账户，看看新上市的游戏</p>
					<button class="sbutton" @click="changeType">登录</button>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	export default{
		name:'login-register',
		data(){
			return {
				isLogin:false,
				emailError: false,
				passwordError: false,
				existed: false,
				form:{
					username:'',
					useremail:'',
					userpwd:''
				}
			}
		},
		methods:{
			changeType() {
				this.isLogin = !this.isLogin
				this.form.username = ''
				this.form.useremail = ''
				this.form.userpwd = ''
			},
			login() {
				const self = this;
				if (self.form.useremail != "" && self.form.userpwd != "") {
					self.$axios({
						method:'post',
						url: 'api/user/login',
						data: {
							email: self.form.useremail,
							password: self.form.userpwd
						}
					})
					.then( res => {
						switch(res.data.result){
							case 0:
								alert("登录成功！");
								break;
							case -1:
								this.emailError = true;
								break;
							case 1:
								this.passwordError = true;
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
			register(){
				const self = this;
				if(self.form.username != "" && self.form.useremail != "" && self.form.userpwd != ""){
					self.$axios({
						method:'post',
						url: 'api/user/register',
						data: {
							username: self.form.username,
							email: self.form.useremail,
							password: self.form.userpwd,
							time:'2022-07-25 19:30:00',
							area:'中国-上海'
						}
					})
					.then( res => {
						switch(res.data.result){
							case 0:
								alert("注册成功！");
								this.login();
								break;
							case -1:
								this.existed = true;
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
	.login-register{
		width: 100vw;
		height: 100vh;
		box-sizing: border-box;
		background: linear-gradient(200deg,#FAEBE5,#F6E7F7);
	}
	.contain{
		width: 60%;
		height: 60%;
		position: relative;
		top: 50%;
		left: 50%;
		transform: translate(-50%,-50%);
		background-color: #fff;
		border-radius: 20px;
		box-shadow: 0 0 3px #f0f0f0,
					0 0 6px #f0f0f0;
	}
	.big-box{
		width: 70%;
		height: 100%;
		position: absolute;
		top: 0;
		left: 30%;
		transform: translateX(0%);
		transition: all 1s;
	}
	.big-contain{
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
		height: 30px;
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
		width: 30%;
		height: 100%;
		background: linear-gradient(135deg,rgb(64,64,64),rgb(32,32,32));
		position: absolute;
		top: 0;
		left: 0;
		transform: translateX(0%);
		transition: all 1s;
		border-top-left-radius: inherit;
		border-bottom-left-radius: inherit;
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
		left: 0;
		transition: all 0.5s;
	}
	.small-box.active{
		left: 100%;
		border-top-left-radius: 0;
		border-bottom-left-radius: 0;
		border-top-right-radius: inherit;
		border-bottom-right-radius: inherit;
		transform: translateX(-100%);
		transition: all 1s;
	}
</style>