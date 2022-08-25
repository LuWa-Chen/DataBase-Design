<template>
  <nav class="navBorder"
       id="boxFixed"
       :class="{'is_fixed':isFixed}">
    <el-menu class="el-menu-demo"
             mode="horizontal"
             @select="handleSelect2">
      <el-menu-item index="1"
                    @click="butlink('/')">
        商城
      </el-menu-item>
      <popup v-bind:data="ListIcon"
             num="2"
             title="库" />
      <popup v-bind:data="server"
             num="3"
             title="社区" />
      <popup v-bind:data="User"
             num="4"
             title="用户名" />
      <el-menu-item class="right"
                    index="5">
        心愿单
      </el-menu-item>
    </el-menu>
  </nav>

</template>

<script>

import popup from './popup.vue'

export default {
  data () {
    return {
      activeIndex: '1',
      activeIndex4: '1',
      isFixed: false,
      // myServe: [],
      ListIcon: [
        {
          GameName: "主页",
          icon: "...",
          type: 0,
          url: "#"
        },
        {
          GameName: "收藏",
          icon: "...",
          type: 1,
          url: "#"
        },
        {
          GameName: "下载",
          icon: "...",
          type: 2,
          url: "#"
        }
      ],
      server: [{
        GameName: "主页",
        type: 0,
        size: 4,
        url: "#"
      }, {
        GameName: "讨论",
        type: 1,
        size: 4,
        url: "#"
      }, {
        GameName: "创意工坊",
        type: 2,
        size: 4,
        url: "#"
      }, {
        GameName: "市场",
        type: 3,
        size: 4,
        url: "#"
      }, {
        GameName: "实况直播",
        type: 4,
        size: 4,
        url: "#"
      }],
      User: [{
        GameName: "动态",
        type: 0,
        size: 4,
        url: "#"
      }, {
        GameName: "个人资料",
        type: 1,
        size: 4,
        url: "#"
      }, {
        GameName: "好友",
        type: 2,
        size: 4,
        url: "#"
      }, {
        GameName: "组",
        type: 3,
        size: 4,
        url: "#"
      }, {
        GameName: "内容",
        type: 4,
        size: 4,
        url: "#"
      }, {
        GameName: "徽章",
        type: 5,
        size: 4,
        url: "#"
      }, {
        GameName: "库存",
        type: 6,
        size: 4,
        url: "#"
      }]
    }
  },
  created () {

  },
  mounted () {
    window.addEventListener('scroll', this.handleScroll)
  },

  methods: {
    handleSelect2 (key, keyPath) {
       console.log(key, keyPath)
    },
    butlink (path) {
      this.$router.push(path)
    },
    handleScroll () {
      let scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
      let offsetTop = document.querySelector('#boxFixed').offsetTop
      this.isFixed = scrollTop > offsetTop ? true : false
    }

  },
  components: {
    popup
  }
}

</script>

<style>
.route {
  text-decoration: none;
}
* {
  list-style: none;
  user-select: none;
}
.is_fixed {
  position: fixed;
  z-index: 10;
  top: 0;
  width: 100%;
}
.navBorder {
  z-index: 100;
  width: 100%;
  border-top: 1px solid rgba(255, 255, 255, 0.2);
  border-bottom: 1px solid rgba(255, 255, 255, 0.2);
  height: 60px;
  background-color: #032041;
}
.navBorder .right {
  position: relative;
}
.navBorder .el-menu-demo {
  width: 100%;
  margin: auto;
}
.navBorder .el-menu.el-menu--horizontal.el-menu {
  border: none;
  background-color: rgba(0, 0, 0, 0);
}
.navBorder .el-menu--horizontal > .el-menu-item:not(.is-disabled):focus,
.navBorder .el-menu--horizontal > .el-menu-item:not(.is-disabled):hover,
.navBorder .el-menu--horizontal > .el-submenu .el-submenu__title:hover {
  background-color: rgba(0, 0, 0, 0);
  color: #fff;
}
.navBorder .el-menu--horizontal > .el-submenu.is-active .el-submenu__title {
  border: none;
  color: #909399;
}
.navBorder .el-menu--horizontal > .el-submenu:focus .el-submenu__title,
.navBorder .el-menu--horizontal > .el-submenu:hover .el-submenu__title {
  color: #fff;
}
.navBorder .el-menu--horizontal .el-menu-item .is-active {
  border: none;
  color: #909399;
  min-width: 120px;
}
.navBorder .el-menu--popup {
  padding: 0px;
}
.navBorder .el-menu--horizontal .el-menu--popup .is-active:hover {
  color: #fff;
  background-color: rgba(58, 63, 74, 1);
}
.navBorder .el-submenu .el-menu-item {
  min-width: 120px;
}
.navBorder ul li {
  margin-left: 0px !important;
  display: block !important;
  text-align: left;
}
.navBorder
.el-submenu
.el-menu--horizontal
.el-menu-item:not(.is-disabled):focus,
.navBorder
.el-submenu
.el-menu--horizontal
.el-menu-item:not(.is-disabled):hover {
  background-color: rgba(60, 66, 77, 1);
  border: 0px;
}
.navBorder .el-menu--horizontal > .el-menu-item.is-active {
  border: 0px;
  color: #909399;
}
.navBorder .iconMargin {
  padding-left: 5px;
}
.el-menu--horizontal > .el-submenu .el-submenu__title {
  border-bottom: 0px;
}
</style>

