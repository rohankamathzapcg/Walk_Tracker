// eslint-disable-next-line no-unused-vars
import React, { useEffect, useState } from 'react'
import axios from 'axios'

const Regions = () => {
  const [regions, setRegions] = useState([]);
  const [isSmallScreen, setIsSmallScreen] = useState(window.innerWidth < 768);

  useEffect(() => {
    axios.get("https://localhost:7258/api/Regions")
      .then((result) => {
        setRegions(result.data)
      })
      .catch((err) => console.log(err))

    const handleResize = () => {
      setIsSmallScreen(window.innerWidth < 768);
    }

    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    }
  }, [])

  return (
    <>
      <div className='mt-4 mb-4 container-fluid'>
        <h2 className="text-uppercase text-center mb-5">Walking Details</h2>
        <button className='btn btn-sm btn-lg btn-success mb-3'>Add New Region</button>
        {
          regions.length <= 0 ? <p className='text-center'>No records found</p> :
            (
              <div className="table-responsive">
                <table className="table table-striped table-bordered">
                  <thead>
                    <tr>
                      <th scope="col" className='text-center'>Region</th>
                      <th scope="col" className='text-center'>Code</th>
                      <th scope="col" className='text-center'>Image</th>
                      <th scope="col" className='text-center'>Actions</th>
                    </tr>
                  </thead>
                  <tbody className="table-group-divider">
                    {regions.map((region, index) => {
                      return (
                        <tr key={index}>
                          <th className='text-center'>{region.name}</th>
                          <td className='text-center'>{region.code}</td>
                          <td className='text-center'>{region.imageURL}</td>
                          <td>
                            <div className={`d-flex justify-content-${isSmallScreen ? 'start' : 'center'}`}>
                              <button className="btn btn-warning me-2" style={{ width: '80px' }}>Edit</button>
                              <button className="btn btn-danger" style={{ width: '80px' }}>Delete</button>
                            </div>
                          </td>
                        </tr>
                      )
                    })}
                  </tbody>
                </table>
              </div>
            )
        }
      </div>
    </>
  )
}

export default Regions
