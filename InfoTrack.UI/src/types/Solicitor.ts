export interface Solicitor {
  name: string;
  description: string;
  logoUrl: string;
  Address: Address;
  ContactDetails: ContactDetails;
}

interface Address {
  addressLine1: string;
  location: string;
  postcode: string
}

interface ContactDetails {
  telephone: string;
  website: string
}