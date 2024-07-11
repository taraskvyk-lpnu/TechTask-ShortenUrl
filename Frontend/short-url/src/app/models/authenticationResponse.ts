export interface AuthenticationResponse {
  id: number,
  email: string,
  username: string,
  roles: string[],
  jwtToken: string
}
