import Vue from 'vue'
import VueRouter from 'vue-router'
// import GameInfo from "@/views/GameInfo";

Vue.use(VueRouter)

const originalPush = VueRouter.prototype.push
// 修改 原型对象中的push方法
VueRouter.prototype.push = function push(location,onResolve,onReject) {
  if (onResolve || onReject)
    return originalPush.call(this, location, onResolve, onReject)
  return originalPush.call(this, location).catch(err => err)
}

const routes = [
  {//游戏首页
    path:'/',
    name:'GamePage',
    component: () => import( '@/views/GamePage')
  },
  {//用户注册登录
    path:'/userlogin',
    name:'Login',
    component: () => import( '@/views/LoginRegister')
  },
  {//发行商注册
    path:'/publisher_register',
    name:'P_Register',
    component: () => import( '@/views/PublisherRegister')
  },
  {//搜索页面
    path:'/search',
    name:'SearchResult',
    component: () => import( '@/views/SearchPage')
  },
  {//游戏详情
    path: '/gamedetail',
    name: 'GameDetail',
    component: () => import( '@/views/GameDetail')
  },
  {//库界面游戏详情
    path:'/library_gameinfo',
    name:'GameInfo',
    component: () => import( '@/views/About'),
  },
  {//库界面
    path:'/library',
    name:'Library',
    component: () => import( '@/views/PersonalGamebase'),
  },
  {//游戏推送
    path:'/library/news',
    name:'News',
    component: () => import( '@/views/News'),
  },
  {
    path: '/publisher',
    name: 'PublisherPage',
    component: () => import( '@/views/PublisherPage'),
    children:[
      {
        path: '/selectgame',
        name: 'SelectGame',
        component: () => import( '@/views/SelectGame'),
      },
      {
        path: '/discountgame',
        name: 'DiscountGame',
        component: () => import( '@/views/DiscountGame'),
      },
      {
        path: '/publishgame',
        name: 'PublishGame',
        component: () => import( '@/views/PublishGame'),
      }
    ]
  },
  {//个人主页
    path: '/home',
    name: 'home',
    component: () => import( '@/views/HomePage'),
    children:[
      {
        path: '/account',
        name: 'account',
        component: () => import( '@/views/AccountInfo')
      },
      {
        path: '/profile',
        name: 'profile',
        component: () => import( '@/views/SelfProfile')
      },
      {
        path: '/safety',
        name: 'safety',
        component: () => import( '@/views/SafetySet')
      },
      {
        path: '/friends',
        name: 'friends',
        component: () => import( '@/views/FriendsList')
      },
      {
        path: '/myOrder',
        name: 'myOrder',
        component: () => import( '@/views/MyOrder')
      },
      {
        path: '/CDKey',
        name: 'CDKey',
        component: () => import( '@/views/CDKey')
      }
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
