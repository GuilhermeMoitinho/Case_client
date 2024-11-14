<template>
  <div class="container">
    <h2>Criar Cliente</h2>
    <form @submit.prevent="createCustomer">
      <div class="form-group">
        <label for="companyName">Nome da Empresa</label>
        <input v-model="companyName" type="text" id="companyName" class="form-control" placeholder="Digite o nome da empresa" required />
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
      <button type="submit" class="btn btn-primary">Criar Cliente</button>
      <router-link to="/" class="btn btn-secondary">Voltar a Lista</router-link>
    </form>
  </div>
</template>



<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'
import { CustomerService } from '@/services/CustomerService'

export default defineComponent({
  setup() {
    const router = useRouter()
    const companyName = ref('')
    const companySize = ref('')

    const createCustomer = async () => {
      try {
        await CustomerService.createCustomer(
           companyName.value,
           companySize.value,
        )
        router.push('/')
      } catch (error) {
        alert('Erro ao criar cliente')
        console.error(error)
      }
    }

    return {
      companyName,
      companySize,
      createCustomer,
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
