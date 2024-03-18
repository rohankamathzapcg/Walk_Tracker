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
  const [selectedId, setSelectedId] = useState(null)
  const [updateRegionsData, setUpdateRegionData] = useState({
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

  const handleEdit = (id) => {
    setSelectedId(id)
    for (let i = 0; i < regions.length; i++) {
      if (regions[i].id === id) {
        setUpdateRegionData({
          name: regions[i].name,
          code: regions[i].code,
          imageURL: regions[i].imageURL
        })
      }
    }
  }

  const handleDelete = (id) => {
    setSelectedId(id)
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

  const handleUpdate = () => {
    axios.put(`https://localhost:7258/api/Regions/${selectedId}`, updateRegionsData)
      .then((result) => {

        if (result.status === 200) {
          toast.success("Region updated Successfully", {
            theme: "dark",
            autoClose: 1000,
          });
          handleCloseBtn();
          setTimeout(() => {
            window.location.reload();
          }, 2000);
          console.log(regions)
        } else {
          toast.error("Invalid Data", {
            theme: "dark",
            autoClose: 1000,
          });
        }
      })
      .catch(err => console.log(err))
  }

  const handleRemove = () => {
    axios.delete(`https://localhost:7258/api/Regions/${selectedId}`)
      .then((result) => {
        if (result.status === 200) {
          toast.success("Region deleted Successfully", {
            theme: "dark",
            autoClose: 1000,
          });
          handleCloseBtn();
          setTimeout(() => {
            window.location.reload();
          }, 2000);
          console.log(regions)
        } else {
          toast.error("Something went wrong!!", {
            theme: "dark",
            autoClose: 1000,
          });
        }
      })
      .catch(err => console.log(err))
  }


  return (
    <>
      <ToastContainer />
      <div className='mt-4 mb-4 container-fluid'>
        <h2 className="text-uppercase text-center mb-5">Walking Details</h2>
        <button type="button" className="btn btn-sm btn-lg btn-success mb-3" data-bs-toggle="modal" data-bs-target="#regionModal">
          Add new Regions
        </button>

        {/* Create Modal */}
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

        {/* Edit Modal */}
        <div className="modal fade" id="editRegionModal" tabIndex={-1} aria-labelledby="editRegionModalLabel" aria-hidden="true">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="editRegionModalLabel">Edit Region</h1>
                <button type="button" ref={closeRef} className="btn-close shadow-none" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">
                <div className="input-group mb-3">
                  <span className="input-group-text">Region Name</span>
                  <input type="text" className="form-control shadow-none" value={updateRegionsData.name} aria-label="Region Name" onChange={(e) => setUpdateRegionData({ ...updateRegionsData, name: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Region Code</span>
                  <input type="text" className="form-control shadow-none" value={updateRegionsData.code} aria-label="Region Code" onChange={(e) => setUpdateRegionData({ ...updateRegionsData, code: e.target.value })} />
                </div>
                <div className="input-group mb-3">
                  <input type="file" className="form-control shadow-none" aria-label="Region Image" onChange={(e) => setUpdateRegionData({ ...updateRegionsData, imageURL: e.target.files[0].name })} />
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary shadow-none" data-bs-dismiss="modal">Close</button>
                <button type="button" onClick={handleUpdate} className="btn btn-primary shadow-none">Update Region</button>
              </div>
            </div>
          </div>
        </div>

        {/* Delete Modal */}
        <div className="modal fade" id="deleteRegionModal" tabIndex={-1} aria-labelledby="deleteRegionModalLabel" aria-hidden="true">
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="deleteRegionModalLabel">Delete Region</h1>
                <button type="button" ref={closeRef} className="btn-close shadow-none" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">
                <p>Do you want to delete this specific Region ?</p>
                <div className="modal-footer">
                  <button type="button" className="btn btn-secondary shadow-none" data-bs-dismiss="modal">Close</button>
                  <button type="button" onClick={handleRemove} className="btn btn-danger shadow-none">Delete Region</button>
                </div>
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
                              <button type="button" className="btn btn-warning me-2" style={{ width: '80px' }} data-bs-toggle="modal" data-bs-target="#editRegionModal" onClick={() => handleEdit(region.id)}>
                                Edit
                              </button>
                              <button className="btn btn-danger" style={{ width: '80px' }} data-bs-toggle="modal" data-bs-target="#deleteRegionModal" onClick={() => handleDelete(region.id)}>Delete</button>
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
