export interface Activity {
  id: string
  title: string
  date: string
  description: string
  category: Category
  address: Address
}

export interface Category {
  name: string
  description: string
}

export interface Address {
  street: string
  city: string
  state: string
  country: string
  zipCode: string
  venue: string
}
