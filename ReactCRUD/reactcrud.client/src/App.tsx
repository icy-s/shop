import './App.css';
import SchoolList from "./views/SchoolList";
import SchoolDetail from "./views/SchoolDetail";
import SchoolDelete from "./views/SchoolDelete"
import { BrowserRouter, Routes, Route } from "react-router-dom"

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<SchoolList />} />
                <Route path="/schoolDetails/:id" element={<SchoolDetail />} />
                <Route path="/schoolDelete/:id" element={<SchoolDelete />} />
            </Routes>
        </BrowserRouter>
    )
}

export default App;