<template>
  <div class="customer-detail" v-if="customer">
    <h1>{{ customer.companyName }} Details</h1>
    <p><strong>Company Size:</strong> {{ customer.companySize }}</p>
    <router-link to="/">Back to list</router-link>
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
