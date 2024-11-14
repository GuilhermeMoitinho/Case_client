import { createRouter, createWebHistory } from 'vue-router'
import CustomersList from '@/views/CustomerList.vue'
import CreateCustomer from '@/views/CreateCustomer.vue'
import EditCustomer from '@/views/EditCustomer.vue'
import CustomerDetail from '@/views/CustomerDetail.vue'
import DeleteCustomer from '@/views/DeleteCustomer.vue'

const routes = [
  {
    path: '/',
    name: 'customers-list',
    component: CustomersList,
  },
  {
    path: '/create',
    name: 'create-customer',
    component: CreateCustomer,
  },
  {
    path: '/customer/:id',
    name: 'customerDetail',
    component: CustomerDetail,
    props: true,
  },
  {
    path: '/customer/:id/edit',
    name: 'edit-customer',
    component: EditCustomer,
    props: true,
  },
  {
    path: '/customer/:id/delete',
    name: 'delete-customer',
    component: DeleteCustomer,
    props: true,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
