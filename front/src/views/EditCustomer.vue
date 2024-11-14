<template>
  <div class="container">
    <h2>Editar Cliente</h2>
    <form @submit.prevent="editCustomer">
      <div class="form-group">
        <label for="companyName">Nome da Empresa</label>
        <input v-model="companyName" type="text" id="companyName" class="form-control" required />
      </div>
      <div class="form-group">
        <label for="companySize">Tamanho da Empresa</label>
        <select v-model="companySize" id="companySize" class="form-control" required>
          <option disabled value="">Selecione o Tamanho</option>
          <option value="Small">Pequena</option>
          <option value="Medium">Média</option>
          <option value="Large">Grande</option>
        </select>
      </div>
      <button type="submit" class="btn btn-primary">Salvar Alterações</button>
      <router-link to="/" class="btn btn-secondary">Cancelar</router-link>
    </form>
  </div>
</template>


<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { CustomerService } from '@/services/CustomerService'

export default defineComponent({
  setup() {
    const router = useRouter()
    const route = useRoute()
    const companyName = ref('')
    const companySize = ref('')

    const getCustomer = async () => {
      const id = route.params.id as string
      try {
        const customer = await CustomerService.getCustomerById(id)
        companyName.value = customer.companyName
        companySize.value = customer.companySize
      } catch (error) {
        console.error('Error fetching customer:', error)
      }
    }

    onMounted(() => {
      getCustomer()
    })

    const editCustomer = async () => {
      const id = route.params.id as string
      try {
        await CustomerService.updateCustomer(id,
          companyName.value,
          companySize.value,
        )
        router.push('/')
      } catch (error) {
        console.error('Error updating customer:', error)
      }
    }

    return {
      companyName,
      companySize,
      editCustomer,
    }
  },
})
</script>

<style scoped>
.container {
  margin-top: 20px;
}

h2 {
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 15px;
}

button {
  margin-top: 10px;
}
</style>
