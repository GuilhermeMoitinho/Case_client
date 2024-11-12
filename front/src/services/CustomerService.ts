import axios from 'axios'

const API_URL = 'http://localhost:8080/api/v1/customers/'

interface CustomerResponse {
  id: string
  companyName: string
  companySize: string
}

export const CustomerService = {
  async getAllCustomers(offset: number = 0, limit: number = 15): Promise<CustomerResponse[]> {
    try {
      const response = await axios.get(`${API_URL}?offset=${offset}&limit=${limit}`)
      console.log(response)
      return response.data.data
    } catch (error) {
      throw new Error(`Failed to fetch customers: ${error.message}`)
    }
  },

  async getCustomerById(id: string): Promise<CustomerResponse> {
    try {
      const response = await axios.get('http://localhost:8080/api/v1/customers/' + id)
      console.log(response)
      return response.data.data
    } catch (error) {
      throw new Error(`Failed to fetch customer with ID ${id}: ${error.message}`)
    }
  },

  async createCustomer(companyName: string, companySize: string): Promise<CustomerResponse> {
    try {
      console.log(companyName, companySize)
      const response = await axios.post('http://localhost:8080/api/v1/customers/', {
        companyName,
        companySize,
      })
      console.log(response)
      return response.data.data
    } catch (error) {
      throw new Error(`Failed to create customer: ${error.message}`)
    }
  },

  async updateCustomer(
    id: string,
    companyName: string,
    companySize: string,
  ): Promise<CustomerResponse> {
    try {
      const response = await axios.put('http://localhost:8080/api/v1/customers/' + id, {
        companyName,
        companySize,
      })
      return response.data.data
    } catch (error) {
      throw new Error(`Failed to update customer with ID ${id}: ${error.message}`)
    }
  },

  async deleteCustomer(id: string): Promise<void> {
    try {
      await axios.delete('http://localhost:8080/api/v1/customers/' + id)
    } catch (error) {
      throw new Error(`Failed to delete customer with ID ${id}: ${error.message}`)
    }
  },
}
