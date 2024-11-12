<template>
  <div class="customer-list">
    <h1>Customers</h1>

    <div class="actions">
      <router-link to="/create" class="btn btn-primary">Create New Customer</router-link>
    </div>

    <div v-if="customers.length">
      <ul>
        <li v-for="customer in customers" :key="customer.id" class="customer-item">
          <div class="customer-details">
            <router-link
              :to="{ name: 'customerDetail', params: { id: customer.id } }"
              class="customer-name"
            >
              {{ customer.companyName }} ({{ customer.companySize }})
            </router-link>
            <router-link
              :to="{ name: 'edit-customer', params: { id: customer.id } }"
              class="btn btn-warning btn-sm"
            >
              Edit
            </router-link>
          </div>
        </li>
      </ul>
    </div>

    <div v-else>
      <p>No customers found.</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { CustomerService } from '@/services/CustomerService'

export default defineComponent({
  name: 'CustomerList',
  setup() {
    const customers = ref<any[]>([])

    const fetchCustomers = async () => {
      try {
        customers.value = await CustomerService.getAllCustomers()
      } catch (error) {
        console.error('Error fetching customers:', error)
      }
    }

    onMounted(fetchCustomers)

    return { customers }
  },
})
</script>

<style scoped>
.customer-list {
  padding: 20px;
}

h1 {
  font-size: 2em;
  margin-bottom: 20px;
}

.actions {
  margin-bottom: 20px;
}

.btn {
  padding: 8px 12px;
  border-radius: 4px;
  color: white;
  text-decoration: none;
}

.btn-primary {
  background-color: #007bff;
}

.btn-primary:hover {
  background-color: #0056b3;
}

.btn-warning {
  background-color: #ffc107;
}

.btn-warning:hover {
  background-color: #e0a800;
}

ul {
  list-style-type: none;
  padding: 0;
}

.customer-item {
  padding: 10px 0;
  border-bottom: 1px solid #ccc;
}

.customer-details {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.customer-name {
  font-weight: bold;
  color: #007bff;
  text-decoration: none;
}

.customer-name:hover {
  text-decoration: underline;
}
</style>
