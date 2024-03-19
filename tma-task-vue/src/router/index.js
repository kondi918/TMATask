import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import WarehouseView from '@/views/WarehouseView.vue'
import RequestsView from '@/views/RequestsView.vue'
import AdminView from '@/views/AdminView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/admin',
    name: 'admin',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: AdminView
  },
  {
  path: '/login',
  name: 'login',
  component: () => import('../views/LoginView.vue')
  },
  {
  path: '/warehouse',
  name: 'warehouse',
  component: WarehouseView
  },
  {
    path:'/requests',
    name: 'requests',
    component: RequestsView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
