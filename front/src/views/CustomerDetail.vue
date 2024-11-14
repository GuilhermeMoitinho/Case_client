<template>
  <div class="container customer-detail">
    <h1>Detalhes de {{ customer.companyName }}</h1>
    <p><strong>Tamanho da Empresa:</strong> {{ customer.companySize }}</p>
    <router-link to="/" class="btn btn-secondary">Voltar a Lista</router-link>
  </div>
</template>


<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { CustomerService } from '@/services/CustomerService'

export default defineComponent({
  name: 'CustomerDetail',
  setup() {
    const route = useRoute()
    const customer = ref<any | null>(null)

    const fetchCustomer = async () => {
      try {
        customer.value = await CustomerService.getCustomerById(route.params.id as string)
      } catch (error) {
        console.error('Error fetching customer:', error)
      }
    }

    onMounted(fetchCustomer)

    return { customer }
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
