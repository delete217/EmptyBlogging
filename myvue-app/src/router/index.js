import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Login from '@/components/Login'
import AllTags from '@/components/TagViews/AllTag'
import ViewTag from '@/components/TagViews/Tag'
import Nav from '@/components/Navi/Nav'
import Test from '@/components/Test'
import ArticleBody from '@/components/ArticleViews/ArticleBody'
import ArticleDetails from '@/components/ArticleViews/ArticleDetails'
import AboutAuthor from '@/components/help/About.vue'
import SearchArticle from '@/components/ArticleViews/SearchArticle'
import UserInfo from '@/components/help/UserInfo'
import AllCategory from '@/components/CategoryViews/AllCategory'
import ViewCategory from '@/components/CategoryViews/Category'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Index',
      component: Login
    },
    {
      path: '/Home',
      name: 'Home',
      component: Home,
    },
    {
      path: '/Navi', redirect: { name: 'AllArticles' },
      name: 'Nav',
      component: Nav,
      children: [
        {
          path: 'AllTags',
          name: 'AllTags',
          component: AllTags, // 使用 AllTagsParent 作为父级组件
          children: [
            {
              // path: 'ViewTag',
              path: '',
              name: 'ViewTag',
              component: ViewTag,
              props: route => ({ tagId: route.query.tagId }),
              beforeEnter: (to, from, next) => {
                // 如果没有 tagId 参数，则重定向到默认的路径
                if (!to.query.tagId) {
                  next({ name: 'ViewTag', query: { tagId: 5 } });
                } else {
                  next();
                }
              }
            },
          ]
        },
        {
          path: 'AllCategory',
          name: 'AllCategory',
          component: AllCategory,
          children: [
            {
              path: '',
              name: 'ViewCategory',
              component: ViewCategory,
              props: route => ({ categoryId: route.query.categoryId }),
              beforeEnter: (to, from, next) => {
                if (!to.query.categoryId) {
                  next({ name: 'ViewCategory', query: { categoryId: 1 } })
                } else {
                  next();
                }
              }
            },
          ]
        },
        {
          path: 'AllArticles',
          name: 'AllArticles',
          component: ArticleBody,
        },
        {
          path: 'View',
          name: 'ArticleDetails',
          component: ArticleDetails,
          props: route => ({ articleId: route.query.articleId })
        },
        {
          path: 'Test',
          name: 'Test',
          component: Test
        },
        {
          path: 'AboutAuthor',
          name: 'AboutAuthor',
          component: AboutAuthor
        },
        {
          path: 'SearchArticle',
          name: 'SearchArticle',
          component: SearchArticle,
          props: route => ({ articleName: route.query.articleName })
        },
        {
          path: 'UserInfo',
          name: 'UserInfo',
          component: UserInfo
        },

      ]
    }
  ]
})
