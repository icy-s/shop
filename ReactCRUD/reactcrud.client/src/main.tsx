import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import SchoolList from './views/SchoolList.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <SchoolList />
  </StrictMode>,
)
