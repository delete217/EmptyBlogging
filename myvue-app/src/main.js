// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import mavonEditor from 'mavon-editor';
import 'mavon-editor/dist/css/index.css';
import showdown from 'showdown';
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';

Vue.use(ElementUI);
Vue.use(mavonEditor);
Vue.config.productionTip = false
Vue.prototype.converter = new showdown.Converter();
// 设置全局标题
document.title = '𝓔mpty空空'
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
