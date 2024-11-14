<template>
    <div class="customer-detail" v-if="customer">
      <h1>Deseja realmente excluir o cliente?</h1>
      <h1>{{ customer.companyName }} Details</h1>
      <p><strong>Company Size:</strong> {{ customer.companySize }}</p>
      <button @click="deleteCustomer" type="button" class="btn btn-primary">Deletar cliente</button>
      <router-link to="/">Voltar a lista</router-link>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, onMounted, ref } from 'vue'
  import { useRoute, useRouter } from 'vue-router'
  import { CustomerService } from '@/services/CustomerService'
  
  export default defineComponent({
    name: 'CustomerDetail',
    setup() {
      const route = useRoute()
      const router = useRouter()
      const customer = ref<any | null>(null)
  
      const fetchCustomer = async () => {
        try {
          customer.value = await CustomerService.getCustomerById(route.params.id as string)
        } catch (error) {
          console.error('Error fetching customer:', error)
        }
      }
  
      onMounted(fetchCustomer)
  
      const deleteCustomer = async () => {
        const id = route.params.id as string
        try {
          await CustomerService.deleteCustomer(id)
          router.push('/')
        } catch (error) {
          console.error('Error deleting customer:', error)
        }
      }
  
      return { customer, deleteCustomer }
    },
  })
  </script>
  
  <style scoped>
  .customer-detail {
    padding: 20px;
  }
  
  h1 {
    font-size: 2em;
  }
  
  a {
    text-decoration: none;
    color: #007bff;
  }
  
  a:hover {
    text-decoration: underline;
  }
  </style>
  