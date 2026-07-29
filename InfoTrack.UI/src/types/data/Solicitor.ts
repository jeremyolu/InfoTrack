export default interface Solicitor {
  name: string;
  description: string;
  logoUrl: string;
  address: Address;
  contactDetails: ContactDetails;
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