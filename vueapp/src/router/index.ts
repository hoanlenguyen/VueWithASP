import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    }
  ]
})

export const redirectRoute = () : void => {
    const currentRoute = router.currentRoute.value;
    const routeLocation = currentRoute.matched?.length > 1 ? currentRoute.matched[1].path : '/';
    if (routeLocation == currentRoute.fullPath) {
      router.go(0);
    } else {
      router.push(routeLocation);
    }
}

export default router
