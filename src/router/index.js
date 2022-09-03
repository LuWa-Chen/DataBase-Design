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
  {//管理员登录
    path:'/adminlogin',
    name:'AdminLogin',
    component: () => import( '@/views/AdminLogin')
  },
  {//管理员界面
    path:'/adminpage',
    name:'AdminPage',
    component: () => import( '@/views/AdministratorPage')
  },
  {//管理用户库界面
    path:'/adminBasePage',
    name:'AdminBasePage',
    component: () => import( '@/views/AdministratorGamebase')
  },
  {//查看更多
    path:'/viewMore',
    name:'ViewMore',
    component: () => import( '@/views/ViewMore'),
  },
  {//搜索页面
    path:'/search',
    name:'SearchResult',
    component: () => import( '@/views/SearchPage')
  },
  {//游戏详情
    path: '/gamedetail',
    name: 'GameDetail',
    component: () => import( '@/views/GameDetail'),
    props: true,
  },
  {
    path: '/cart',
    name: 'ShoppingCart',
    component: () => import( '@/views/ShoppingCart'),
  },
  {
    path: '/column',
    name: 'GameColumn',
      component: () => import( '@/views/GameColumns'),
    props: true,
  },
  {//库界面游戏详情
    path:'/basedetail',
    name:'GameBaseDetail',
    component: () => import( '@/views/GameBaseDetail'),
  },
  {//库界面
    path:'/library',
    name:'Library',
    component: () => import( '@/views/PersonalGamebase'),
  },
  {//发行商界面
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
        path: '/ChangeGameInfo',
        name: 'ChangeGameInfo',
        component: () => import( '@/views/ChangeGameInfo'),
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
      },
      {
        path: '/publisherprofile',
        name: 'PublisherProfile',
        component: () => import( '@/views/PublisherProfile'),
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
