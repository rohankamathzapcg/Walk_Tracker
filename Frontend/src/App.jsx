import { BrowserRouter, Routes, Route } from 'react-router-dom'
import 'react-toastify/dist/ReactToastify.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import Navigationbar from '../src/Components/Navigationbar';
import Footer from '../src/Components/Footer';
import HomePage from '../src/Components/HomePage';
import Regions from '../src/Components/Regions';
import Difficulty from '../src/Components/Difficulty';
import Walk from '../src/Components/Walk';

const App = () => {
  return (
    <>
      <BrowserRouter>
        <Navigationbar/>
        <Routes>

          <Route path='/' element={<HomePage/>} />
          <Route path='/regions' element={<Regions/>} />
          <Route path='/difficulty' element={<Difficulty/>} />
          <Route path='/walks' element={<Walk/>} />

        </Routes>
        <Footer />
      </BrowserRouter>
    </>
  )
}

export default App
