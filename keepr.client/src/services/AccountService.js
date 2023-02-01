import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async updateAccount(editing) {
    try {
      const res = await api.put('/account', editing)
      // logger.log('edit profile', res.data)
      AppState.account = res.data
    } catch (error) {
      logger.error('Failed to edit account', error)
    }
  }

  async getMyVaults() {
    try {
      const res = await api.get("/account/vaults")
      AppState.myVaults = res.data
    } catch (error) {
      logger.error(error)
      Pop.toast(error.message, 'error')
    }
  }
}

export const accountService = new AccountService()
