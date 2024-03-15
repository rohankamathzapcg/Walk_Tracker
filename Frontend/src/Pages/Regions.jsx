// eslint-disable-next-line no-unused-vars
import React, { useEffect, useRef, useState } from 'react'
import axios from 'axios'
import { ToastContainer, toast } from 'react-toastify';

const Regions = () => {
  const [regions, setRegions] = useState([]);
  const [regionsData, setRegionData] = useState({
    name: "",
    code: "",
    imageURL: "",
  })
  const [isSmallScreen, setIsSmallScreen] = useState(window.innerWidth < 768);
  const closeRef = useRef(null);

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

  const handleCloseBtn = () => {
    if (closeRef.current) {
      closeRef.current.click();
    }
  }

  const handleSubmit = () => {

    axios.post("https://localhost:7258/api/Regions", regionsData)
      .then((result) => {
        if (result.status === 201) {
          toast.success("New Region Added Successfully", {
            theme: "dark",
            autoClose: 1000,
          });
          setRegionData({
            name: "",
            code: "",
            imageURL: ""
          });
          handleCloseBtn();
          setTimeout(() => {
            window.location.reload();
          }, 2000);

        } else {
          toast.error("Invalid Data", {
            theme: "dark",
            autoClose: 1000,
          });
        }
      })
      .catch(() => toast.error("Something went wrong! Try Again Later", {
        theme: "dark",
        autoClose: 1000,
      }))
  }

  return (
    <>
      <ToastContainer />
      <div className='mt-4 mb-4 container-fluid'>
        <h2 className="text-uppercase text-center mb-5">Walking Details</h2>
        <button type="button" className="btn btn-sm btn-lg btn-success mb-3" data-bs-toggle="modal" data-bs-target="#regionModal">
          Add new Regions
        </button>

        {/* Modal */}
        <div className="modal fade" id="regionModal" tabIndex={-1} aria-labelledby="regionModalLabel" aria-hidden="true">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="regionModalLabel">Add New Region</h1>
                <button type="button" ref={closeRef} className="btn-close shadow-none" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">
                <div className="input-group mb-3">
                  <span className="input-group-text">Region Name</span>
                  <input type="text" className="form-control shadow-none" value={regionsData.name} aria-label="Region Name" onChange={(e) => setRegionData({ ...regionsData, name: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Region Code</span>
                  <input type="text" className="form-control shadow-none" value={regionsData.code} aria-label="Region Code" onChange={(e) => setRegionData({ ...regionsData, code: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <input type="file" className="form-control shadow-none" aria-label="Region Image" onChange={(e) => setRegionData({ ...regionsData, imageURL: e.target.files[0].name })} />
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary shadow-none" data-bs-dismiss="modal">Close</button>
                <button type="button" onClick={handleSubmit} className="btn btn-primary shadow-none">Add Region</button>
              </div>
            </div>
          </div>
        </div>

        {
          regions.length <= 0 ? <p className='text-center'>No records found</p> :
            (
              <div className="mb-4 table-responsive">
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
